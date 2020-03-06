using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace AgentAssignment
{
    
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly Agent _agentNina = new Agent("001", "Nina", "Assassination", "UpperVolta");
        private readonly Agent _agentJames = new Agent("007", "James", "Assassination", "Majesty Secret Service");

        public ObservableCollection<Agent> AgentListObs { get; private set; }

        public MainWindowViewModel()
        {
            AgentListObs = new ObservableCollection<Agent>
            {
                _agentNina,
                _agentJames
            };
            CurrentAgent = AgentListObs[_selectedIndex];
        }

        private int _selectedIndex = 0;

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                if (_selectedIndex != value)
                {
                    _selectedIndex = value;
                    Notify();
                }
            }
        }

        private Agent _selectedAgent;

        public Agent CurrentAgent
        {
            get { return _selectedAgent; }
            set
            {
                if (_selectedAgent != value)
                {
                    _selectedAgent = value;
                    Notify();
                }
            }
        }
        

        public event PropertyChangedEventHandler PropertyChanged;

        private void Notify([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public void AddNewAgent()
        {
            var agent = new Agent("","","","");
            AgentListObs.Add(agent);
            CurrentAgent = AgentListObs[AgentListObs.Count - 1];
        }
    }
}
