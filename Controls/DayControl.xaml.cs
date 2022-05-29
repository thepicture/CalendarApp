using System;
using System.Windows;
using System.Windows.Controls;

namespace CalendarApp.Controls
{
    /// <summary>
    /// Interaction logic for DayControl.xaml
    /// </summary>
    public partial class DayControl : UserControl
    {


        public DateTime? Date
        {
            get { return (DateTime?)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        public static readonly DependencyProperty DateProperty =
            DependencyProperty.Register("Date",
                                        typeof(DateTime?),
                                        typeof(DayControl),
                                        new PropertyMetadata(default));

        public DayControl()
        {
            InitializeComponent();
        }
    }
}
