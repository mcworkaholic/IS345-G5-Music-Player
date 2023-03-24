#include <Windows.h>
#include <Mmdeviceapi.h>
#include <Endpointvolume.h>
#include <iostream>
#include <string>

using namespace std;

int main(int argc, char* argv[]) {

    // Get the name of the desired audio device from command line arguments
    string deviceName = argv[1];

    // Initialize the Windows Core Audio API
    CoInitialize(NULL);

    // Get a handle to the default audio endpoint
    IMMDeviceEnumerator* enumerator;
    IMMDevice* defaultDevice;
    endpointVolume->GetDefaultAudioEndpoint(EDataFlow::eRender, ERole::eMultimedia, &defaultDevice);

    // Get a collection of all audio endpoints
    IMMDeviceCollection* deviceCollection;
    enumerator->EnumAudioEndpoints(EDataFlow::eRender, DEVICE_STATE_ACTIVE, &deviceCollection);

    // Iterate through the collection and set the default endpoint to the desired device
    UINT numDevices;
    deviceCollection->GetCount(&numDevices);
    for (UINT i = 0; i < numDevices; i++) {
        IMMDevice* device;
        deviceCollection->Item(i, &device);

        // Get the name of the current device
        LPWSTR deviceNameW;
        device->GetId(&deviceNameW);
        string currentDeviceName = wstring(deviceNameW);

        // If the current device matches the desired device, set it as the default
        if (currentDeviceName.find(deviceName) != string::npos) {
            endpointVolume->SetDefaultEndpoint(device);
            break;
        }

        // Release the current device object
        device->Release();
    }

    // Release the device collection and default device objects
    deviceCollection->Release();
    defaultDevice->Release();

    // Uninitialize the Windows Core Audio API
    CoUninitialize();

    return 0;
}