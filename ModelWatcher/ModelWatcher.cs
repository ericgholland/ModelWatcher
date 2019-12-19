//This is a test header
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;

namespace ModelWatcher
{
    public class ModelWatcher<TModel> : IModelWatcher
    {
        TModel model;
        INotifyPropertyChanged inpcModel;
        List<string> propertyNames = new List<string>();
        List<Rule<TModel>> ruleList = new List<Rule<TModel>>();
        ModelWatcherContext modelWatcherContext;

        public ModelWatcher(ModelWatcherContext modelWatcherContext, TModel modelObject, string propertyName)
        {
            this.modelWatcherContext = modelWatcherContext;
            this.model = modelObject;
            propertyNames.Add(propertyName);

            if (modelObject is INotifyPropertyChanged)
            {
                inpcModel = (INotifyPropertyChanged)model;
                inpcModel.PropertyChanged += InpcModel_PropertyChanged;
            }
        }

        public void ExecuteAllRules()
        {
            foreach (Rule<TModel> rule in ruleList)
            {
                if (rule.Execute(model))
                {
                    modelWatcherContext.AddResult(new Result());
                }
            }
        }

        public Rule<TModel> ErrorWhen(Func<TModel, bool> condition)
        {
            Rule<TModel> rule = new Rule<TModel>(condition);
            ruleList.Add(rule);
            return rule;
        }

        private void InpcModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!propertyNames.Contains(e.PropertyName))
                return;

            ExecuteAllRules();
        }
    }
}