using System;
using System.ComponentModel;

namespace Talent.Services.Converter
{
    public class TypeConverter
    {
        private readonly System.ComponentModel.TypeConverter[] _typeCastPriority;

        public TypeConverter()
        {
            _typeCastPriority = new[]
            {
                TypeDescriptor.GetConverter(typeof(int)),
                TypeDescriptor.GetConverter(typeof(long)),
                TypeDescriptor.GetConverter(typeof(double)),
                TypeDescriptor.GetConverter(typeof(string))
            };
        }

        public object ConvertToAppropriateType(string input)
        {
            foreach (var converter in _typeCastPriority)
            {
                if (converter.IsValid(input))
                    return converter.ConvertFrom(input);
            }
            throw new Exception("Didn't find any appropriate type to cast.");
        }
    }
}