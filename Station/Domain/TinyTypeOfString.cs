namespace Terminal.Domain
{
    public class TinyTypeOfString
    {
        private string _value;
        private readonly int _maxLength;
        public string value => _value;

        protected TinyTypeOfString( int maxLength, string defaultValue , string name )
        {
            // used to limit string length
            _maxLength = maxLength;
            
            if (string.IsNullOrEmpty(name))
            {
                _value = defaultValue;
            }
            else if (name.Trim() == string.Empty)
            {
                _value = defaultValue;
            }
            else
            {
                // remove leading and trailing spaces
                _value = name.Trim();
            }

            // limit to _maLength characters
            if (_value.Length > _maxLength)
            {
                _value = _value.Substring(0, _maxLength).Trim();
            }
        }
    }
}