using System.Linq.Expressions;

namespace ScrumProject.Models.DataAccess
{
    public class QueryOptions<T>
    {
        //lambda expression for filtering
        public Expression<Func<T, Object>> OrderBy { get; set;}
        public Expression<Func<T, bool>> Where { get; set; }


        //string array to hold any related entities and write only property to remove spaces and split at commas
        private string[] includes;
        public string Includes
        {
            set => includes = value.Replace(" ", "").Split(',');
        }

        //return includes array or empty array if null
        public string[] GetIncludes() => includes ?? new string[0];

        //readonly values
        public bool HasWhere => Where != null;
        public bool HasOrderBy => OrderBy != null;
    }
}
