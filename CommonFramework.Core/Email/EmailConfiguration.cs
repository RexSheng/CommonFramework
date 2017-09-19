using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CommonFramework.Core.Email
{
    public class EmailConfiguration : IEmailConfiguration
    {
        private readonly IEmailSettingOption _setting;
        public EmailConfiguration(IEmailSettingOption setting)
        {
            _setting = setting;
        }
        public void Config(Expression<Action<IEmailSettingOption>> option)
        {
            var action = option.Compile();
            action.Invoke(_setting);
        }

        public EmailSettings GetSettingOption()
        {
            return _setting.getSetting();
        }
    }
}
