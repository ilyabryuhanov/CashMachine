using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using CashMachine.Common.CrossCutting.Interfaces;

namespace CashMachine.Common.CrossCutting.Helpers
{
    /// <summary>
    /// Класс с набором методов, упрощающих реализацию интерфейса <see cref="INotifyPropertyChanged" /> 
    /// </summary>
    public static class NotifyPropertyChangedHelper
    {
        /// <summary>
        /// Verifies that a property name exists in this ViewModel. This method
        /// can be called before the property is used, for instance before
        /// calling RaisePropertyChanged. It avoids errors when a property name
        /// is changed but some places are missed.
        /// </summary>
        /// <remarks>This method is only active in DEBUG mode.</remarks>
        /// <param name="obj">Source object.</param>
        /// <param name="propertyName">The name of the property that will be
        /// checked.</param>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        private static void VerifyPropertyName(IObservableObject obj, string propertyName)
        {
            var myType = obj.GetType();

            if (!string.IsNullOrEmpty(propertyName)
                && myType.GetProperty(propertyName) == null)
            {
                // ReSharper disable once SuspiciousTypeConversion.Global
                var descriptor = obj as ICustomTypeDescriptor;

                if (descriptor?.GetProperties()
                    .Cast<PropertyDescriptor>()
                    .Any(property => property.Name == propertyName) == true)
                {
                    return;
                }

                throw new ArgumentException("Property not found", propertyName);
            }
        }

        /// <summary>
        /// Raises the PropertyChanging event if needed.
        /// </summary>
        /// <remarks>If the propertyName parameter
        /// does not correspond to an existing property on the current class, an
        /// exception is thrown in DEBUG configuration only.</remarks>
        /// <param name="obj">Source object.</param>
        /// <param name="propertyName">(optional) The name of the property that
        /// changed.</param>
        public static void RaisePropertyChanging(
            this IObservableObject obj, [CallerMemberName] string propertyName = null)
        {
            VerifyPropertyName(obj, propertyName);

            var handler = obj.PropertyChangingHandler;
            handler?.Invoke(obj, new PropertyChangingEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the PropertyChanged event if needed.
        /// </summary>
        /// <remarks>If the propertyName parameter
        /// does not correspond to an existing property on the current class, an
        /// exception is thrown in DEBUG configuration only.</remarks>
        /// <param name="obj">Source object.</param>
        /// <param name="propertyName">(optional) The name of the property that
        /// changed.</param>
        public static void RaisePropertyChanged(
            this IObservableObject obj, [CallerMemberName] string propertyName = null)
        {
            VerifyPropertyName(obj, propertyName);

            var handler = obj.PropertyChangedHandler;
            handler?.Invoke(obj, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Assigns a new value to the property. Then, raises the
        /// PropertyChanged event if needed. 
        /// </summary>
        /// <typeparam name="T">The type of the property that
        /// changed.</typeparam>
        /// <param name="obj">Source object.</param>
        /// <param name="field">The field storing the property's value.</param>
        /// <param name="newValue">The property's value after the change
        /// occurred.</param>
        /// <param name="propertyName">(optional) The name of the property that
        /// changed.</param>
        /// <returns>True if the PropertyChanged event has been raised,
        /// false otherwise. The event is not raised if the old
        /// value is equal to the new value.</returns>
        public static bool Set<T>(
            this IObservableObject obj,
            ref T field, T newValue,
            [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
                return false;

            // ReSharper disable ExplicitCallerInfoArgument
            RaisePropertyChanging(obj, propertyName);
            // ReSharper restore ExplicitCallerInfoArgument

            field = newValue;

            // ReSharper disable ExplicitCallerInfoArgument
            RaisePropertyChanged(obj, propertyName);
            // ReSharper restore ExplicitCallerInfoArgument

            return true;
        }


        /// <summary>
        /// Assigns a new value to the property. Then, raises the
        /// PropertyChanged event if needed, and than do Action
        /// </summary>
        /// <typeparam name="T">The type of the property that
        /// changed.</typeparam>
        /// <param name="obj">Source object.</param>
        /// <param name="field">The field storing the property's value.</param>
        /// <param name="newValue">The property's value after the change
        /// occurred.</param>
        /// <param name="action"></param>
        /// <param name="propertyName">(optional) The name of the property that
        /// changed.</param>
        /// <returns>True if the PropertyChanged event has been raised,
        /// false otherwise. The event is not raised if the old
        /// value is equal to the new value.</returns>
        public static void Set<T>(
            this IObservableObject obj,
            ref T field, T newValue,
            Action<T> action,
            [CallerMemberName] string propertyName = null)
        {
            // ReSharper disable ExplicitCallerInfoArgument
            if (Set(obj, ref field, newValue, propertyName))
                // ReSharper restore ExplicitCallerInfoArgument
                action(newValue);
        }
    }
}