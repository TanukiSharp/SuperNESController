using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SuperNESController.Core;

namespace SuperNESController
{
    public class ExtensionContainer
    {
        public Buttons ButtonsMask { get; set; }
        public IExtension Extension { get; }

        public ExtensionContainer(IExtension extension)
        {
            if (extension == null)
                throw new ArgumentNullException(nameof(extension));

            ButtonsMask = Buttons.None;
            Extension = extension;
        }
    }

    public class Module
    {
        public string ModuleFilename { get; }
        public Assembly Assembly { get; private set; }
        public ExtensionContainer[] Extensions { get; private set; }

        public Module(string moduleFilename)
        {
            if (string.IsNullOrWhiteSpace(moduleFilename))
                throw new ArgumentException(nameof(moduleFilename));

            ModuleFilename = moduleFilename;
        }

        public bool Initialize(TextWriter writer)
        {
            byte[] moduleData;

            try
            {
                moduleData = File.ReadAllBytes(ModuleFilename);
            }
            catch (Exception ex)
            {
                writer.WriteLine($"Impossible to load file '{ModuleFilename}'");
                writer.WriteLine(ex);

                return false;
            }

            try
            {
                Assembly = Assembly.Load(moduleData);
            }
            catch (Exception ex)
            {
                writer.WriteLine($"Impossible to load assembly '{ModuleFilename}'");
                writer.WriteLine(ex);

                return false;
            }

            Type extensionType = typeof(IExtension);

            IEnumerable<Type> types = Assembly.GetTypes()
                .Where(x => extensionType.IsAssignableFrom(x))
                .Where(x => x.IsAbstract == false);

            var extensions = new List<ExtensionContainer>();

            foreach (Type type in types)
            {
                IExtension ext = null;

                try
                {
                    ext = (IExtension)Activator.CreateInstance(type);
                }
                catch (Exception ex)
                {
                    writer.WriteLine($"Impossible to load extention '{type.FullName}' from file '{ModuleFilename}'");
                    writer.WriteLine(ex);
                    continue;
                }

                try
                {
                    ext.Initialize(ModuleFilename);
                }
                catch (Exception ex)
                {
                    writer.WriteLine($"Extension '{ext.Name}' version '{ext.Version}' failed to initialize");
                    writer.WriteLine(ex);
                    continue;
                }

                extensions.Add(new ExtensionContainer(ext));
            }

            Extensions = extensions.ToArray();

            return extensions.Count > 0;
        }
    }

    public class ModuleManager
    {
        public Module[] Modules { get; private set; }

        private string configFile;

        public void Initialize(string[] moduleFilenames, TextWriter writer)
        {
            configFile = Path.ChangeExtension(Assembly.GetEntryAssembly().Location, ".xml");

            Modules = moduleFilenames
                .Select(x => new Module(x))
                .Where(x => x.Initialize(writer))
                .ToArray();

            try
            {
                LoadConfiguration();
            }
            catch
            {
            }
        }

        private void LoadConfiguration()
        {
            if (File.Exists(configFile) == false)
                return;

            XDocument doc;

            try
            {
                doc = XDocument.Load(configFile);
            }
            catch
            {
                return;
            }

            foreach (XElement module in doc.Elements("modules").Elements("module"))
            {
                Module existingModule = null;

                try
                {
                    string moduleFileName = (string)module.Attribute("filename");
                    string moduleAssemblyName = (string)module.Attribute("assembly");

                    existingModule = Modules.FirstOrDefault(m =>
                            Path.GetFileName(m.ModuleFilename) == moduleFileName &&
                            m.Assembly.FullName == moduleAssemblyName);
                }
                catch
                {
                }

                if (existingModule == null)
                    continue;

                foreach (XElement ext in module.Elements("extension"))
                {
                    try
                    {
                        var extType = (string)ext.Attribute("type");
                        var extName = (string)ext.Attribute("name");
                        var extVersion = (int)ext.Attribute("version");

                        ExtensionContainer existingExtension = existingModule.Extensions.FirstOrDefault(x =>
                            x.Extension.GetType().FullName == extType &&
                            x.Extension.Name == extName &&
                            x.Extension.Version == extVersion);

                        if (existingExtension == null)
                            continue;

                        var buttons = (int)ext.Attribute("buttons");
                        existingExtension.ButtonsMask = (Buttons)buttons;
                    }
                    catch { }
                }
            }
        }

        public void SaveConfiguration()
        {
            var doc = new XDocument(
                new XElement("modules",
                    Modules.Select(x => new XElement("module",
                        new XAttribute("filename", Path.GetFileName(x.ModuleFilename)),
                        new XAttribute("assembly", x.Assembly.FullName),
                        x.Extensions.Select(y => new XElement("extension",
                            new XAttribute("type", y.Extension.GetType().FullName),
                            new XAttribute("name", y.Extension.Name),
                            new XAttribute("version", y.Extension.Version),
                            new XAttribute("buttons", (int)y.ButtonsMask)))))));

            doc.Save(configFile);
        }
    }
}
