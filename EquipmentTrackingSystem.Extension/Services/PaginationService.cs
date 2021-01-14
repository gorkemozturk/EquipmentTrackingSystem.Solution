using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquipmentTrackingSystem.Extension.Services
{
	public class PaginationService<T> : List<T>
	{
		public int Page { get; private set; }
		public int Pages { get; private set; }
		public int Size { get; private set; }
		public int Total { get; private set; }

		public bool HasPrevious => Page > 1;
		public bool HasNext => Page < Pages;

		public PaginationService(List<T> items, int count, int pageNumber, int pageSize)
		{
			Total = count;
			Size = pageSize;
			Page = pageNumber;
			Pages = (int)Math.Ceiling(count / (double)pageSize);

			AddRange(items);
		}

		public static PaginationService<T> ToPagedList(List<T> source, int page, int size)
		{
			var count = source.Count();
			var items = source.Skip((page - 1) * size).Take(size).ToList();

			return new PaginationService<T>(items, count, page, size);
		}
	}
}
