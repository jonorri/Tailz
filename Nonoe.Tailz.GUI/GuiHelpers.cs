using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nonoe.Tailz.GUI
{
    using System.Drawing;
    using System.Windows.Forms;

    using ScintillaNET;

    public static class GuiHelpers
    {
        public static DialogResult InputBox(ref string pluginName, ref string pluginContent)
        {
            Label lblPluginName = new Label
                                      {
                                          Text = "Plugin name",
                                          AutoSize = true,
                                          Bounds = new Rectangle(9, 20, 372, 13)
                                      };

            Label lblPluginContent = new Label
                                         {
                                             AutoSize = true,
                                             Text = "Plugin content",
                                             Bounds = new Rectangle(9, 66, 372, 13)
                                         };

            TextBox txtPluginName = new TextBox
                                        {
                                            Text = "Hello world",
                                            Bounds = new Rectangle(12, 36, 372, 20),
                                            Anchor = AnchorStyles.Left | AnchorStyles.Top
                                        };

            ScintillaNET.Scintilla txtPluginContent = new Scintilla
                                                          {
                                                              ConfigurationManager = { Language = "ruby" },
                                                              Text =
                                                                  "class Script " + Environment.NewLine
                                                                  + "\tdef Run(text)" + Environment.NewLine
                                                                  + "\t\t text = 'hello world: ' + text"
                                                                  + Environment.NewLine + "\t end"
                                                                  + Environment.NewLine + "end",
                                                              Bounds = new Rectangle(12, 82, 372, 120),
                                                              Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom,
                                                              AutoSize = true
                                                          };

            Button buttonOk = new Button
                                  {
                                      Text = "OK",
                                      DialogResult = DialogResult.OK,
                                      Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                                      Bounds = new Rectangle(228, 218, 75, 23),
                                  };

            Button buttonCancel = new Button
                                      {
                                          Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                                          Text = "Cancel",
                                          DialogResult = DialogResult.Cancel,
                                          Bounds = new Rectangle(309, 218, 75, 23),
                                      };

            Form inputBox = new Form
                                {
                                    Text = "Create plugin",
                                    ClientSize = new Size(396, 253),
                                    MinimumSize = new Size(396, 253),
                                    FormBorderStyle = FormBorderStyle.SizableToolWindow,
                                    StartPosition = FormStartPosition.CenterParent,
                                    MinimizeBox = false,
                                    MaximizeBox = false,
                                    AcceptButton = buttonOk,
                                    CancelButton = buttonCancel
                                };

            inputBox.Controls.AddRange(new Control[] { lblPluginName, txtPluginName, lblPluginContent, txtPluginContent, buttonOk, buttonCancel });

            DialogResult dialogResult = inputBox.ShowDialog();
            pluginName = txtPluginName.Text;
            pluginContent = txtPluginContent.Text;
            return dialogResult;
        }
    }
}
