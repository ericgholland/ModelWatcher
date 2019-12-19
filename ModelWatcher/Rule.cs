//This is a test header
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelWatcher
{
    public class Rule<TModel>
    {
        Func<TModel, bool> condition;

        public Rule(Func<TModel, bool> condition)
        {
            this.condition = condition;
        }

        public bool Execute(TModel model)
        {
            return condition(model);
        }
    }
}