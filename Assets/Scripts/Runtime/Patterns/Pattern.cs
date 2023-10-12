using System.Linq;

namespace GorillaGong.Runtime.Patterns
{
    public class Pattern
    {
        public int[] Values => _values;
        private readonly int[] _values;

        public Pattern(int[] values)
        {
            _values = values;
        }

        public bool IsInputValid(int[] inputs)
        {
            if (_values.Length != inputs.Length)
            {
                return false;
            }

            if (_values.Length == 1)
            {
                return _values[0] == inputs[0];
            }
            
            return _values.Intersect(inputs).Count() == _values.Length;
        }

        public override string ToString()
        {
            return $"{{{string.Join(", ", _values)}}}";
        }
    }
}