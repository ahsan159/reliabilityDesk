﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace ReliabilityReportPrinting
{

    /// <summary>
    /// This class is taken from the example provided by 
    /// https://stackoverflow.com/questions/263551/databind-the-source-property-of-the-webbrowser-in-wpf
    /// this is dependency injection method implmeneted to make 
    /// web browser accept binable sources. which by default it does not.
    /// </summary>
    internal class BrowserBehavior
    {

        public static readonly DependencyProperty BindableSourceProperty =
    DependencyProperty.RegisterAttached("BindableSource", typeof(string), typeof(BrowserBehavior), new UIPropertyMetadata(null, BindableSourcePropertyChanged));

        public static string GetBindableSource(DependencyObject obj)
        {
            return (string)obj.GetValue(BindableSourceProperty);
        }

        public static void SetBindableSource(DependencyObject obj, string value)
        {
            obj.SetValue(BindableSourceProperty, value);
        }

        public static void BindableSourcePropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            WebBrowser browser = o as WebBrowser;
            if (browser != null)
            {
                string uri = e.NewValue as string;
                browser.Source = !String.IsNullOrEmpty(uri) ? new Uri(uri) : null;
                //browser.Navigate(uri);
            }
        }
    }
}

