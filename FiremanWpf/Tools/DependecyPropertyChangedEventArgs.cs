// //Fireman->FiremanWpf->DependecyPropertyChangedEventArgs.cs
// //andreygolubkow Андрей Голубков

using System;
using System.Windows;

namespace FiremanWpf.Tools
{
    public class DependencyPropertyChangedEventArgs<T> : EventArgs
    {
        public DependencyPropertyChangedEventArgs(DependencyPropertyChangedEventArgs e)
        {
            NewValue = (T)e.NewValue;
            OldValue = (T)e.OldValue;
            Property = e.Property;
        }

        public T NewValue { get; private set; }
        public T OldValue { get; private set; }
        public DependencyProperty Property { get; private set; }
    }
}
