using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace SolSoft.Utilities.UI.Wpf.Commands
{
	public class DelegateCommand : ICommand
	{
		private readonly Action<object> m_action;
		private readonly Func<object, bool> m_predicate;

		public DelegateCommand(Action<object> action) : this(action, null) { }
		public DelegateCommand(Action<object> action, Func<object, bool> canExecutePredicate)
		{
			if (action == null)
				throw new ArgumentNullException("action");

			this.m_action = action;
			this.m_predicate = canExecutePredicate;
		}

		public bool CanExecute(object parameter)
		{
			return m_predicate == null || m_predicate(parameter);
		}

		public event EventHandler CanExecuteChanged
		{
			add
			{
				CommandManager.RequerySuggested += value;
			}
			remove
			{
				CommandManager.RequerySuggested -= value;
			}
		}

		public void Execute(object parameter)
		{
			m_action(parameter);
		}
	}
}
