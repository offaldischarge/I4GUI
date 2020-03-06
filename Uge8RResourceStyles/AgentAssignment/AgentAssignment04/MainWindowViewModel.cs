using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Xml.Serialization;
using Prism.Commands;


namespace AgentAssignment
{
    
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly Agent _agentNina = new Agent("001", "Nina", "Assassination", "UpperVolta");
        private readonly Agent _agentJames = new Agent("007", "James", "Assassination", "Majesty Secret Service");

        public ObservableCollection<Agent> AgentListObs { get; private set; }
        private string filename = "";

        private DispatcherTimer _dt;

        public MainWindowViewModel()
        {
            AgentListObs = new ObservableCollection<Agent>
            {
                _agentNina,
                _agentJames
            };
            CurrentAgent = AgentListObs[_selectedIndex];

            _dt = new DispatcherTimer();
            _dt.Interval = TimeSpan.FromSeconds(1);
            _dt.Tick += _dt_Tick;
            _dt.Start();

        }

        private string _agentDateTime;

        public string AgentDateTime 
        {   
            get => _agentDateTime;
            set
            {
                if (_agentDateTime != value)
                {
                    _agentDateTime = DateTime.Now.ToString();
                    Notify();
                }
            }
        }

        private void _dt_Tick(object sender, EventArgs e)
        {
            AgentDateTime = DateTime.Now.ToString();
        }

        private int _selectedIndex = 0;

        public int SelectedIndex
        {
            get => _selectedIndex;
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
            get => _selectedAgent;
            set
            {
                if (_selectedAgent != value)
                {
                    _selectedAgent = value;
                    Notify();
                }
            }
        }

        private ICommand _previousButtonCommand;
        public ICommand PreviousButtonCommand => _previousButtonCommand ??
                                                 (_previousButtonCommand = new DelegateCommand(
                                                         () => --SelectedIndex, 
                                                         () => 0 < SelectedIndex)
                                                     .ObservesProperty(() => SelectedIndex));

        private ICommand _nextButtonCommand;
        public ICommand NextButtonCommand => _nextButtonCommand ??
                                             (_nextButtonCommand = new DelegateCommand(
                                                     () => ++SelectedIndex,
                                                     () => SelectedIndex < (AgentListObs.Count - 1))
                                                 .ObservesProperty(() => SelectedIndex));

        private ICommand _addNewCommand;
        public ICommand AddNewCommand => _addNewCommand ?? (_addNewCommand = new DelegateCommand(AddNewAgent));

        private ICommand _deleteAgentCommand;
        public ICommand DeleteAgentCommand => _deleteAgentCommand ?? 
                                              (_deleteAgentCommand = new DelegateCommand(
                                                  () => AgentListObs.RemoveAt(SelectedIndex),
                                                  () => AgentListObs.Count > 0)
                                                  .ObservesProperty(() => AgentListObs.Count));

        private ICommand _changeColorCommand;

        public ICommand ChangeColorCommand => _changeColorCommand ?? (_changeColorCommand = new DelegateCommand<string>(ChangeColor));

        public void ChangeColor(string chosenColor)
        {
            SolidColorBrush newBrush = SystemColors.WindowBrush;
            if (chosenColor != null)
            {
                if (chosenColor != "Default")
                {
                    newBrush = new SolidColorBrush((Color) ColorConverter.ConvertFromString(chosenColor));
                }
            }

            Application.Current.Resources["BackgroundBrushStyle"] = newBrush;
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
