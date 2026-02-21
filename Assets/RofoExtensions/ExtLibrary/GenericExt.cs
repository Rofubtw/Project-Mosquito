using System.Collections.Generic;
using System.Linq;

namespace Extensions
{
	public static class GenericExt
	{

		public static bool ContainsAny<T>( this T self, IEnumerable<T> values )
		{
			return self.ContainsAny( values.ToArray() );
		}

		public static bool ContainsAny<T>( this T self, params T[] values )
		{
			foreach ( var n in values )
			{
				if ( self.Equals( n ) )
				{
					return true;
				}
			}
			return false;
		}
	}
}