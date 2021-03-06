﻿using System.Windows;
using System.Windows.Input;

namespace ProductionMan.Desktop
{

    /// <summary>
    /// Interaction logic for UserEditorWindow.xaml
    /// </summary>
    public partial class UserEditorWindow
    {

        public UserEditorWindow()
        {
            InitializeComponent();
            Loaded += (sender, e) => MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }


        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            var model = DataContext as UserEditorWindowViewModel;
            if (model != null && model.User != null)
            {
                model.User.Password = PasswordCtrl.Password;
            }
        }
    }
}
