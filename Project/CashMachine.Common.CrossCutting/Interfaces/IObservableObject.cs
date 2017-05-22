using System.ComponentModel;

namespace CashMachine.Common.CrossCutting.Interfaces
{
    /// <summary>
    /// Интерфейс необходимый для отделения реализации реализации
    /// INotifyPropertyChanged и INotifyPropertyChanging от классов, которым эта реализация требуется.
    /// </summary>
    /// <remarks>Реализовывать только явно!</remarks>
    public interface IObservableObject : INotifyPropertyChanged, INotifyPropertyChanging
    {
        /// <summary>
        /// Обработчик события завершения изменения значения свойства.
        /// </summary>
        PropertyChangedEventHandler PropertyChangedHandler { get; }

        /// <summary>
        /// Обработчик события начала изменения значения свойства.
        /// </summary>
        PropertyChangingEventHandler PropertyChangingHandler { get; }
    }
}