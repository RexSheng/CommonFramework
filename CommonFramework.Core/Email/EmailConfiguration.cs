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
        private List<EmailSettings> _allSettings = new List<EmailSettings>();
        private readonly IEmailSettingOption _setting;
        public EmailConfiguration(IEmailSettingOption setting)
        {
            _setting = setting;
        }
        public void Config(Expression<Action<IEmailSettingOption>> option)
        {
            var action = option.Compile();
            action.Invoke(_setting);
            _allSettings.Add(_setting.getSetting());
            _setting.clearSetting();
        }

        public EmailSettings GetSettingOption(string key=null)
        {
            var a= _allSettings.Where(m=>string.IsNullOrEmpty(key)?m.IsDefault:m.SettingKey.Equals(key)).FirstOrDefault();
            if (a != null) {
                return a;
            }
            else
            {
                return _allSettings.FirstOrDefault();
            }
        }
    }
}
