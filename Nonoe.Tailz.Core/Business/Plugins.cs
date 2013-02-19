namespace Nonoe.Tailz.Core
{
    using System;
    using System.Collections.ObjectModel;

    using Nonoe.Tailz.Core.Objects;

    public class Plugins
    {
        public delegate void PluginAddedHandler(object sender, string pluginName, string rubyScript);
        public event PluginAddedHandler Plugin;

        public delegate void PluginChangedHandler(object sender);
        public event PluginChangedHandler PluginChanged;

        public void CreatePlugin(string pluginName, string rubyScript)
        {
            // TODO: Store this somewhere so the user doesn't have to create all the plugins at each invocation.
            this.Plugin(this, pluginName, rubyScript);
        }

        public Collection<Plugin> GetPlugins()
        {
            throw new NotImplementedException();
        }

        public void ChangePluginActivity(string pluginName, string pluginContent, bool active)
        {
            // TODO: Update this somewhere so the user doesn't have to create all the plugins at each invocation.
        }
    }
}
