namespace Nonoe.Tailz.Core
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Nonoe.Tailz.Core.DAL;
    using Nonoe.Tailz.Core.Objects;

    public class Plugins
    {
        public delegate void PluginAddedHandler(object sender, string pluginName, string rubyScript);
        public event PluginAddedHandler Plugin;

        public delegate void PluginChangedHandler(object sender);
        public event PluginChangedHandler PluginChanged;

        private PluginDAL pluginDal = new PluginDAL();

        public Plugins()
        {
            
        }

        public void CreatePlugin(string pluginName, string rubyScript)
        {
            this.pluginDal.Add(pluginName, rubyScript);
            this.Plugin(this, pluginName, rubyScript);
        }

        public IList<Plugin> GetPluginsByActivity(bool activity)
        {
            return this.pluginDal.GetPluginsByActivity(activity);
        }

        public void ChangePluginActivity(string pluginName, string pluginContent, bool active)
        {
            this.pluginDal.ChangePluginActivity(pluginName, active);
        }

        public void DeletePlugin(string pluginName, bool activity)
        {
            this.pluginDal.Delete(pluginName);
        }
    }
}
