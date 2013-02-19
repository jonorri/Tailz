namespace Nonoe.Tailz.Core.Ruby
{
    using Microsoft.Scripting.Hosting;

    public static class RubyRunner
    {
        public static string Run(string scriptText, string text)
        {
            // TODO: Question whether I don't need to handle ruby exception here.
            ScriptEngine engine = IronRuby.Ruby.CreateEngine();
            ScriptSource scripts = engine.CreateScriptSourceFromString(scriptText);
            CompiledCode code = scripts.Compile();
            ScriptScope scope = engine.CreateScope();
            code.Execute(scope);

            dynamic globals = engine.Runtime.Globals;
            dynamic script = globals.Script.@new();

            // Call the Run method in the script.
            dynamic result = script.Run(text);
            return result.ToString();
        }
    }
}
