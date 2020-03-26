using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using ItViteaLetterFrequenties.Model;
using Microsoft.Win32;
using System.IO;

namespace ItViteaLetterFrequenties.Viewmodel
{
    public class VMLetterInfo : INotifyPropertyChanged
    {
        private string _TextInput, _InfoLabel;
        private LetterInfoList _LetterList;
        private bool _IsChecked1;
        public VMLetterInfo()
        {
            LetterList = new LetterInfoList();
        }
        #region Public Properties
        public LetterInfoList LetterList
        {
            get { return _LetterList; }
            set
            {
                _LetterList = value;
                OnPropertyChanged("LetterList");
            }
        }

        public string TextInput
        {
            get { return _TextInput; }
            set
            {
                _TextInput = value;
                OnPropertyChanged("TextInput");
            }
        }
        public string InfoLabel
        {
            get { return _InfoLabel; }
            set
            {
                _InfoLabel = value;
                OnPropertyChanged("InfoLabel");
            }
        }
        public bool IsChecked1
        {
            get { return _IsChecked1; }
            set
            {
                _IsChecked1 = value;
                OnPropertyChanged("IsChecked1");
            }
        }
        #endregion

        #region Commands
        public ICommand GetListCommand
        {
            get
            {
                return new RelayCommand(SetList);
            }
        }
        public ICommand UpdateListItem
        {
            get
            {
                return new RelayCommand(ChangeListItem);
            }
        }
        public ICommand OpenFileCommand
        {
            get
            {
                return new RelayCommand(OpenFile);
            }
        }
        public ICommand LoadFileCommand
        {
            get
            {
                return new RelayCommand(LoadFile);
            }
        }
        public ICommand ToggleCharactersCommand
        {
            get
            {
                return new RelayCommand(ToggleCharacters);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hard coded mostly for testing if list will update when item is changed.
        /// </summary>
        public void ChangeListItem()
        {
            LetterList[1].Letter = 'p';
        }
        public void SetList()
        {
            MakeList(TextInput);
        }
        public void MakeList(string str)
        {
            if (str != null)
            {
                LetterList.FillLetterList(str);
                SetFrequencyList();
            }
            else
                TextInput = "Error.";
        }
        public void SetFrequencyList()
        {
            double dblSum = LetterList.Sum(ltrList => ltrList.Count);

            foreach (var item in LetterList)
            {
                item.Frequency = (item.Count / dblSum);
            }
        }
        private void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
                TextInput = File.ReadAllText(openFileDialog.FileName);
        }
        private void LoadFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                MakeList(File.ReadAllText(openFileDialog.FileName));
                InfoLabel = openFileDialog.SafeFileName.ToString();
            }
        }
        private void ToggleCharacters()
        {
            if(IsChecked1)
            {
                InfoLabel = "On";
            }
            else
            {
                InfoLabel = "Off";
            }
        }
        private void GridHideRows()
        {
            foreach (LetterInfo ltrInfos in LetterList)
            {
                
            }
        }
        #endregion

        #region INotifyPropertyChanged Members  
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
