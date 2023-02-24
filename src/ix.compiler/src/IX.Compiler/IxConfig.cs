// Ix.Compiler
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using Newtonsoft.Json;
using Polly;
using System.IO;
using Ix.Compiler.Exceptions;

namespace Ix.Compiler;

/// <summary>
/// Provides configuration setting for the IX project.
/// </summary>
public class IxConfig : ICompilerOptions
{
    /// <summary>
    /// Creates new instance of IxConfig object.
    /// </summary>
    [Obsolete("Use 'Create IxConfig' instead.")]
    public IxConfig()
    {
            
    }

    /// <summary>
    /// Ix config file name.
    /// </summary>
    public const string CONFIG_FILE_NAME = "ix.config.json";

   
    private string _outputProjectFolder = "ix";

    /// <summary>
    /// Gets or sets the output folder for the Ix project.
    /// </summary>
    public string OutputProjectFolder
    {
        get => _outputProjectFolder.Replace("\\", Path.DirectorySeparatorChar.ToString());
        set => _outputProjectFolder = value;
    }

    
    private string _axProjectFolder;

    /// <summary>
    /// Gets or sets the output folder for the Ix project.
    /// </summary>
    [JsonIgnore]
    public string AxProjectFolder
    {
        get => _axProjectFolder.Replace("\\",Path.DirectorySeparatorChar.ToString());
        set => _axProjectFolder = value;
    }

    /// <summary>
    /// Gets updated or creates default config for given AX project.
    /// </summary>
    /// <param name="directory">AX project directory</param>
    /// <returns>Ix configuration for given AX project.</returns>
    public static IxConfig UpdateAndGetIxConfig(string directory, ICompilerOptions cliCompilerOptions = null)
    {
        var ixConfigFilePath = Path.Combine(directory, CONFIG_FILE_NAME);

        IxConfig? ixConfig = null;

        if (File.Exists(ixConfigFilePath))
        {
            using (StreamReader r = new StreamReader(ixConfigFilePath))
            {
                ixConfig = JsonConvert.DeserializeObject<IxConfig>(r.ReadToEnd());
            }
        }

        if (ixConfig != null)
        {
            ixConfig.AxProjectFolder = directory;
            OverridesFromCli(ixConfig, cliCompilerOptions);
        }

        using (StreamWriter file = File.CreateText(ixConfigFilePath))
        {
            ixConfig = ixConfig == null ? new IxConfig() { AxProjectFolder = directory } : ixConfig;
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, ixConfig);
        }

        using (StreamReader r = new StreamReader(ixConfigFilePath))
        {
            ixConfig = JsonConvert.DeserializeObject<IxConfig>(r.ReadToEnd());
        }

        if (ixConfig != null)
        {
            ixConfig.AxProjectFolder = directory;
        }

        return ixConfig;
    }

   
    public static IxConfig RetrieveIxConfig(string ixConfigFilePath)
    {
        try
        {
            using StreamReader r = new StreamReader(ixConfigFilePath);
            var config = JsonConvert.DeserializeObject<IxConfig>(r.ReadToEnd());
            if (config != null)
            {
                var fi = new FileInfo(ixConfigFilePath);
                if (fi.DirectoryName != null) config.AxProjectFolder = fi.DirectoryName;
            }

            return config;
        }
        catch (Exception ex)
        {
            throw new FailedToReadIxConfigurationFileException($"Unable to process '{ixConfigFilePath}'", ex);
        }
        
    }

    private static void OverridesFromCli(ICompilerOptions fromConfig, ICompilerOptions? fromCli)
    {
        // No CLI params
        if (fromCli == null)
            return;

        // Items to override from the CLI
        fromConfig.OutputProjectFolder = fromCli.OutputProjectFolder ?? fromConfig.OutputProjectFolder;
    }
}