using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ViewModels.BaseViewModels
{
	public class BaseFilterViewModels<T>
	{
		public int StartPage { get; set; }
		public int EndPage { get; set; }
		public int PageCount { get; set; }
		public int PageIndex { get; set; }

		private IEnumerable<T> entities;
		public IEnumerable<T> Entities { get => entities; set => entities = value; }
	}
}
