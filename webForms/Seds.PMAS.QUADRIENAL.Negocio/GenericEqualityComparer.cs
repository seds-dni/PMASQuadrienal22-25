using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class GenericEqualityComparer<T> : IEqualityComparer<T>
    {
        private Func<T, T, Boolean> _comparer;
        private Func<T, int> _hashCodeEvaluator;
        public GenericEqualityComparer(Func<T, T, Boolean> comparer)
        {
            _comparer = comparer;
        }

        public GenericEqualityComparer(Func<T, T, Boolean> comparer, Func<T, int> hashCodeEvaluator)
        {
            _comparer = comparer;
            _hashCodeEvaluator = hashCodeEvaluator;
        }

        #region IEqualityComparer<T> Members

        public bool Equals(T x, T y)
        {
            return _comparer(x, y);
        }

        public int GetHashCode(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            if (_hashCodeEvaluator == null)
            {
                return 0;
            }
            return _hashCodeEvaluator(obj);
        }

        #endregion
    }

}
