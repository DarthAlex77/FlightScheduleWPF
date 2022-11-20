using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FlightScheduleWPF.Models
{
    public static class Extensions
    {
        public static void AddRange<T>(this ObservableCollection<T> oc, IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            foreach (T item in collection)
            {
                oc.Add(item);
            }
        }
    }
}