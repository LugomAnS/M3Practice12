using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace M3Practice12.ViewModels.Base
{
    /// <summary>
    /// Базовый класс ViewModel реализующий интерфейс INotifyPropertyChanged и сеттер Set<T>
    /// </summary>
    internal class ViewModelBase: INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanger([CallerMemberName] string property = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));


        /// <summary>
        /// Устанавливает значение поля и сообщает об изменении 
        /// </summary>
        /// <typeparam name="T">Обобщение</typeparam>
        /// <param name="field">Поле для установки нового значения</param>
        /// <param name="value">Новое значение</param>
        /// <param name="property">Имя поля</param>
        /// <returns>True если значение изменено, False - при оставлении текущего значения</returns>
        protected bool Set<T>(ref T field, T value, [CallerMemberName]string property=null)
        {
            if (Equals(field, value))
            {
                return false;
            }

            field = value;
            OnPropertyChanger(property);
            return true;
        }
    }
}
