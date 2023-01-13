using M3Practice12.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M3Practice12.Commands
{
    /// <summary>
    /// Команда обработки событий
    /// </summary>
    internal class LambdaCommand : Command
    {
        private readonly Action<object>? execute;
        private readonly Func<object, bool>? canExecute;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="execute">Метод выполнениния команды</param>
        /// <param name="canExecute">Метод проверки возможности выполнения команды</param>
        public LambdaCommand(Action<object> execute, Func<object,bool> canExecute )
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// проверяется возможность выполнения команды
        /// </summary>
        /// <param name="parameter">Зависимый объект</param>
        /// <returns>True если выполнение возможно, False при невозможности выполнения команды</returns>
        public override bool CanExecute(object? parameter)
            => canExecute?.Invoke(parameter) ?? true;

        /// <summary>
        /// Выполняемые операции
        /// </summary>
        /// <param name="parameter">Зависимый объект</param>
        public override void Execute(object? parameter)
            => execute?.Invoke(parameter);
    }
}
