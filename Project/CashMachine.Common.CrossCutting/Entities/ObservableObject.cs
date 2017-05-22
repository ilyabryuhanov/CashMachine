using System.ComponentModel;
using CashMachine.Common.CrossCutting.Interfaces;

namespace CashMachine.Common.CrossCutting.Entities
{
    /// <summary>
    /// A base class for objects of which the properties must be observable.
    /// </summary>
    public class ObservableObject : IObservableObject
    {
        /// <summary>
        /// Occurs after a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Provides access to the PropertyChanged event handler to Helpers through IObservableObject.
        /// </summary>
        PropertyChangedEventHandler IObservableObject.PropertyChangedHandler => PropertyChanged;

        /// <summary>
        /// Occurs before a property value changes.
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        /// Provides access to the PropertyChanging event handler to Helpers through IObservableObject.
        /// </summary>
        PropertyChangingEventHandler IObservableObject.PropertyChangingHandler => PropertyChanging;
    }
}