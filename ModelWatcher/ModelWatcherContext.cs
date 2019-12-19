//This is a test header
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ModelWatcher
{
    public class ModelWatcherContext
    {
        List<Result> resultList = new List<Result>();
        List<IModelWatcher> modelWatcherList = new List<IModelWatcher>();

        public ModelWatcher<TModel> Watch<TModel, TProperty>(TModel model, Expression<Func<TModel, TProperty>> expr)
        {
            string propertyName = PropertyNameFromExpression(expr);
            ModelWatcher<TModel> modelWatcher = new ModelWatcher<TModel>(this, model, propertyName);
            modelWatcherList.Add(modelWatcher);
            return modelWatcher;
        }

        public void CheckAllRules()
        {
            foreach (IModelWatcher modelWatcher in modelWatcherList)
                modelWatcher.ExecuteAllRules();
        }

        public IEnumerable<Result> GetResults()
        {
            return resultList;
        }

        public static string PropertyNameFromExpression<TSource, TField>(Expression<Func<TSource, TField>> expr)
        {
            if (object.Equals(expr, null))
            {
                throw new NullReferenceException("PropertyNameFromExpression requires a passed in Expression that is not null");
            }
            MemberExpression mexpr = null;
            if (expr.Body is MemberExpression)
            {
                mexpr = (MemberExpression)expr.Body;
            }
            else if (expr.Body is UnaryExpression)
            {
                mexpr = (MemberExpression)((UnaryExpression)expr.Body).Operand;
            }
            else
            {
                const string Format = "Expression '{0}' not supported.";
                string message = string.Format(Format, expr);
                throw new ArgumentException(message, "expr");
            }
            return mexpr.Member.Name;
        }

        internal void AddResult(Result result)
        {
            resultList.Add(result);
        }
    }
}