using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.PublicClasses
{
	public static class PagingHelper
	{
		public static BasePager Pages(int pageIndex, int entitiesCount, int take, int homePageShow)
		{
			int pageCount = (int)Math.Ceiling(entitiesCount / (double)take);
			int startPage = (pageIndex - homePageShow) <= 0 ? 1 : (pageIndex - homePageShow);
			int endPage = (pageIndex + homePageShow) > pageCount ? pageCount : (pageIndex + homePageShow);
			int skip = (pageIndex - 1) * take;

			var BasePager = new BasePager
			{
				EndPage = endPage,
				StartPage = startPage,
				PageCount = pageCount,
				Skip = skip,
				Take = take,
				PageIndex = pageIndex
				,
			};
			return BasePager;
		}
		public static IEnumerable<T> Pagintion<T>(IEnumerable<T> entities,BasePager pager)
		{
			return entities.Skip(pager.Skip).Take(pager.Take);
		}
	}
}
