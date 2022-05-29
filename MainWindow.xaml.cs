﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CalendarApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<int> Years
        {
            get { return (ObservableCollection<int>)GetValue(YearsProperty); }
            set { SetValue(YearsProperty, value); }
        }

        public static readonly DependencyProperty YearsProperty =
            DependencyProperty.Register("Years",
                                        typeof(ObservableCollection<int>),
                                        typeof(MainWindow),
                                        new PropertyMetadata(
                                            GetDefaultYears()));

        private static object GetDefaultYears()
        {

            ObservableCollection<int> years = new ObservableCollection<int>();
            for (int i = DateTime.Now.Year - 100;
                     i <= DateTime.Now.Year;
                     i++)
            {
                years.Add(i);
            }
            return years;
        }

        public int CurrentYear
        {
            get { return (int)GetValue(CurrentYearProperty); }
            set { SetValue(CurrentYearProperty, value); }
        }

        public static readonly DependencyProperty CurrentYearProperty =
            DependencyProperty.Register("CurrentYear",
                                        typeof(int),
                                        typeof(MainWindow),
                                        new FrameworkPropertyMetadata(DateTime.Now.Year,
                                                                      FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                                                                      OnYearChanged));

        private static void OnYearChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MainWindow window = (MainWindow)d;
            if (e.NewValue == null)
            {
                return;
            }

            if (e.NewValue == e.OldValue)
            {
                return;
            }

            if (!(e.NewValue is int))
            {
                return;
            }

            window.OnCalendarUpdate();
        }

        private void OnCalendarUpdate()
        {
            DGridCalendar.Columns.Clear();
            for (int i = 1; i <= 31; i++)
            {
                DataGridTextColumn column = new DataGridTextColumn
                {
                    Binding = new Binding($"[{i}]"),
                    Header = i
                };
                DGridCalendar.Columns.Add(column);
            }
            List<List<string>> rowsOfColumns = new List<List<string>>();
            for (int i = 1; i <= 12; i++)
            {
                List<string> column = new List<string>
                {
                    new DateTime(CurrentYear, i, 1)
                        .ToString("MMMM")
                };
                for (int j = 1; j <= 31; j++)
                {
                    if (DateTime.DaysInMonth(CurrentYear, i) >= j)
                    {
                        column
                            .Add(new DateTime(CurrentYear, i, j)
                            .ToString("dddd"));
                    }
                    else
                    {
                        column.Add("");
                    }
                }
                rowsOfColumns.Add(column);
            }
            DGridCalendar.ItemsSource = rowsOfColumns;
        }

        public MainWindow()
        {
            InitializeComponent();
            OnCalendarUpdate();
        }
    }
}
