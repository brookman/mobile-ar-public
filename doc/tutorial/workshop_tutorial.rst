===================================
Unity Workshop Preparation Tutorial
===================================

.. To generate the pdf document use pandoc
   $ pandoc --number-sections -V papersize:a4 -o worshop_tutorial.pdf\
     worshop_tutorial.rst
   For the html version rst2html (from docutils) with the provided css template
   located under the css/ folder
   $ python "%TOOLSFOLDER%\docutils-0.15.2\build\scripts-3.7\rst2html.py" --input-encoding="UTF-8" --stylesheet-path="./tutorial/css/mars.css" tutorial/workshop_tutorial.rst tutorial/workshop_tutorial.html


:Author: Samuel Gauthier + Martin Käser, Zühlke Engineering AG
:Email: samuel.gauthier@zuehlke.com, martin.kaeser@zuehlke.com

General Information
===================
Some steps of this guide are only necessary for Android or iOS development. In those cases, the step is marked with the corresponding icon.

* |and| for Android development
* |ios| for iOS development
* |win| for development on Windows 10

General Prerequisites
---------------------
For this course you **need an AR compatible device** and an USB-Cable which connects your device to your laptop.

* |ios| If you have an **iOS-Device** you need **macOS** to deploy the project to the device.
* |osx| XCode 10.3 or 9 (infos later)
* |osx| XCode Commandline Tools (infos later)

Compatible Devices
------------------
* |and| `Android <https://developers.google.com/ar/discover/supported-devices>`_
* |ios| `iOS <https://www.redmondpie.com/ios-11-arkit-compatibility-check-if-your-device-is-compatible-with-apples-new-ar-platform/>`_

Install Unity 
=============
1. Download + Install `Unity Hub <https://store.unity.com/download?ref=personal​>`_
    .. image:: images/unity_hub_download.png
      :alt: Unity Hub download page
      :align: center
      :width: 800px
2. Login + Activate License (if necessary)​
3. Go to *Installs* 
    .. image:: images/install_unity_1.png
      :alt: Unity Hub Installations
      :align: center
      :width: 800px
4. Select *Add*, |unity_version| 

    .. image:: images/install_unity_2.png
       :alt: Unity Hub, Add Unity Version Menu
       :align: center
       :width: 800px

    * |and| Set checkmarks: *Android Build Support*, *Android SDK*, *OpendJDK*
    * |osx| Set checkmark: *iOS build support*
    * |win| Set checkmark: *Visual Studio Community*, if no you do not already have *Visual Studio* installed


    .. image:: images/install_unity_3c.png
       :alt: Unity Hub, Modules Selection
       :align: center
       :width: 800px


|ios| iOS Toolchain
===================
​1. Download and install `XCode 10.3 <https://developer.apple.com/download/more>`_

2. Install `XCode Toolchain <https://www.embarcadero.com/starthere/xe5/mobdevsetup/ios/en/installing_the_commandline_tools.html>`_ 

Install Workshop Files
======================

1. Clone the project or download the ZIP of the `Project Files <https://github.com/brookman/mobile-ar-public>`_ in your desired folder

   .. note:: Example: "*C:\\Users\\USERNAME\\Development\\arworkshop*" | Now Called: "|pf|"​

Verify the Installation
=======================
1. Open Unity Hub

2. Choose *Add* and select the folder of the *Hello-World* project

   .. important:: Unity Hub cannot detect if your project is in a subfolder, so
               make sure to select the right one. E.g.: "|pf|\\Hello-World".

   .. image:: images/add_to_unity_hub.png
      :alt: Add project to the Unity Hub
      :align: center
      :width: 800px

3. |ios| Open up the *Build Settings* (:code:`cmd + shift + b` or via *File > Build
   Settings*)

   I. Make sure that you have the open scene *SampleScene* under
      *Scenes In Build*

   II. Switch the *Platform* to your device type (Anroid or iOS)

   .. image:: images/build_1.png
      :alt: Build settings
      :align: center
      :width: 800px

   III. Open the *Player Settings...*

        a. Open the *Other Settings* menu

        b. Check that the field *Camera Usage Description* is not empty

        c. Under *Target minimum iOS Version* insert the value 11.0

        d. Select *ARM64* from the *Architecture* drop down

        e. Close the window

        .. image:: images/project_settings_1.png
           :alt: Unity Editor Project Settings
           :align: center
           :width: 800px

4. |and| Ensure your device has `Android Debugging <https://www.embarcadero.com/starthere/xe5/mobdevsetup/android/en/enabling_usb_debugging_on_an_android_device.html>`_ enabled

   I. Plug-in the device on your Laptop
   II. Verify that the device is reachable by adb 
       Enter :code:`adb devices -l` in you command line. You should now see something like:

       :code:`8ATX0YYJK              device product:blueline model:Pixel_3 device:blueline transport_id:1`
   III. If the device is shown as *unauthorized*

    Go to the developer options on your mobile device and click "Revoke USB debugging authorization"
    Restart ADB: By entering :code:`adb kill-server` followed by :code:`adb start-server` in your command line

   IV. Reconnect your device and accept the connection on your device screen
   V. Reverify according to step *II.*

5. Open the *Window > Package Manager*

   I. Check that you have the version |arfoundation_version|,
      upgrade if necessary (click on the arrow on the left and then on *See all
      versions*)

   II. |ios| Check that the |arkit_version| is
       installed

   III. |and| Check that the |arcore_version| is
        installed

   .. image:: images/packages.png
      :alt: Unity Editor Packages
      :align: center
      :width: 800px

6. Go the the *Build Settings* again (:code:`cmd + shift + b`) and hit *Build
   and Run*

7. Create a folder called *Builds* in the project folder (the project folder
   contains the *Assets*, *Library*, *Logs*, *ProjectSettings* folders). Give
   your build the name that you like.

   .. image:: images/build_2.png
      :alt: Unity Editor, Save Build under
      :align: center
      :width: 800px

8. |ios| After Unity has finished building, Xcode will start. Select your iphone from
   the active scheme.

   .. image:: images/xcode_3.png
      :width: 400px
      :alt: Xcode active scheme selection
      :align: center

   If you encounter any issues please follow the steps below:

   I. Make sure you have a developer account listed under *Apple IDs* in
      *Xcode* > *Preferences* > *Accounts*

      .. image:: images/xcode_1.png
         :alt: Xcode Accounts selection
         :align: center
         :width: 800px

      Close the *Preference* window

   II. Check *Automatically manage signin* under *Targets* > *Unity-iPhone* >
       *Signing* and select the *Team*

       .. image:: images/xcode_2.png
          :alt: Xcode Automatically manage signing
          :align: center
          :width: 800px

       You may be asked to allow *codesign* to access your keychain

       .. image:: images/code_sign.png
          :alt: Allow codesign to access the keychain
          :align: center
          :width: 800px

   III. Open the *Keychain Access* application, go to your *login* Keychain and
        select the *Certificate* category. Search for an *iPhone Developer*
        certificate and copy its *Organizational Unit*.

        .. image:: images/keychain_1.png
           :alt: Keychain Certificate view
           :align: center
           :width: 800px

        .. image:: images/keychain_2.png
           :alt: Keychain certificate details
           :align: center
           :width: 800px

   IV. Go back to your the Unity *Player Settings* and paste the copied value
       in the field *Signing Team ID* which is located under *Other Settings*

       .. image:: images/project_settings_2.png
          :alt: Unity Editor Project Settings
          :align: center
          :width: 800px

   V. Close the *Player Settings* and *Build And Run* your project again.

9. |ios| Upon completed build, Xcode asks to unlock your iPhone

10. |ios| Trust the developer under *Settings* > *General* > *Device Management* >
    *Developer App* > *Trust "<insert your email here>"* > *Trust*

    .. image:: images/iphone_0.png
       :alt: iPhone Settings
       :width: 250px
    .. image:: images/iphone_1.png
       :alt: iPhone Settings
       :width: 250px
    .. image:: images/iphone_2.png
       :alt: iPhone General
       :width: 250px
    .. image:: images/iphone_3.png
       :alt: iPhone Device Management
       :width: 250px

    .. image:: images/iphone_4.png
       :alt: iPhone Trust developer
       :width: 250px
    .. image:: images/iphone_5.png
       :alt: iPhone Trust developer confirm
       :width: 250px

11. |ios| If the app is not automatically started select it from your home screen

    .. image:: images/iphone_6.png
       :alt: iPhone Settings
       :align: center
       :width: 250px


12. Validate app functions
    
    When the app is running it should detect surfaces of the real world. When tapping the screen a new cube is spawned, which bounces of the surfaces.

.. |win| image:: images/windows.svg
  :width: 18px
  :height: 18px

.. |osx| image:: images/ios.svg
  :width: 18px
  :height: 18px

.. |ios| image:: images/ios.svg
  :width: 18px
  :height: 18px

.. |and| image:: images/android.svg
  :width: 18px
  :height: 18px

.. |pf| replace:: **PROJECT_FOLDER**

.. |unity_version| replace:: **Unity2019.3.0f6**
.. |arfoundation_version| replace:: **3.0.1 ARFoundation XR Plugin**
.. |arkit_version| replace:: **3.0.1 ARKit XR Plugin**
.. |arkit_facetracking_version| replace:: **3.0.1 ARKit Face Tracking**
.. |arcore_version| replace:: **3.0.1 ARCore XR Plugin**
.. |legacy_input_version| replace:: **1.3.8 Legacy Input Helpers**

Credits
-------
* Icons made by `Dave Gandy <https://www.flaticon.com/authors/dave-gandy>`_ from `www.flaticon.com <https://www.flaticon.com/>`_

  * |win|

* Icons made by `Freepik <https://www.flaticon.com/authors/dave-gandy>`_ from `www.flaticon.com <https://www.flaticon.com/>`_

  * |and|
  * |ios|
