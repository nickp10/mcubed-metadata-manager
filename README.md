# mcubed-metadata-manager

Inactive
----
**NOTE** This project is not actively being worked on.

Description
----
Easily manage the metadata of audio files

Setting up a New Projects
----
1. Right click the project and choose Properties.
1. Go to the Signing tab.
1. Check "Sign the assembly".
1. Click the "Choose a strong name key file" dropdown.
1. Click "<New...>".
1. Enter the assembly name (e.g., mCubed) as the "Key file name".
1. Check "Protect my key file with a password".
1. Enter "mCubed3Key" as the password and confirm password.
1. Click OK.
1. Right click the project and choose "Unload Project".
1. Click Yes to save the changes.
1. Right click the project and choose "Edit <PROJECT-NAME>.cspoj".
1. At the bottom of the file, add the following target:
    ```
      <Target Name="BeforePublish">
        <Exec Command="&quot;$(MSBuildProgramFiles32)\Microsoft SDKs\Windows\v7.0A\Bin\signtool.exe&quot; sign /f &quot;$(ProjectDir)$(AssemblyName).pfx&quot; /p mCubed3Key /v &quot;$(ProjectDir)obj\x86\$(ConfigurationName)\$(TargetFileName)&quot;" />
      </Target>
    ```
1. Right click the project and choose "Reload Project".
1. Click Yes to close the file in the editor and Yes to save the changes.

Creating Releases
----
1. In mCubedSecondary.xaml.cs, ensure the Credits region is up to date.
1. In mCubedSecondary.xaml, ensure the About section is up to date.
1. Run all unit tests through Visual Studio to make sure they work.
1. Commit all changes.
1. Set the build mode to Release.
1. Build the solution.
1. Zip the bin/Release directory.
1. Upload the zip file to github.
