#include <Windows.h>
#include <iostream>
#include <string>
#include <thread>

// Function to simulate key press for a given character
void SimulateKeyPress(char character) {
    INPUT input;
    input.type = INPUT_KEYBOARD;
    input.ki.wVk = 0; // Virtual key code (0 for Unicode character)
    input.ki.wScan = character; // Unicode character
    input.ki.dwFlags = KEYEVENTF_UNICODE;
    input.ki.time = 0;
    input.ki.dwExtraInfo = GetMessageExtraInfo();

    // Simulate key press
    SendInput(1, &input, sizeof(INPUT));
}

int main(int argc, char* argv[]) {
    if (argc != 2) {
        std::cout << "Usage: " << argv[0] << " <string_to_type>\n";
        return 1;
    }

    std::string inputString = argv[1];

    // Iterate through each character and simulate key press
    for (char character : inputString) {
        // Wait for 1 millisecond
        std::this_thread::sleep_for(std::chrono::milliseconds(100));
        SimulateKeyPress(character);
    }

    // Release any pressed keys
    INPUT input;
    input.type = INPUT_KEYBOARD;
    input.ki.wVk = 0;
    input.ki.wScan = 0;
    input.ki.dwFlags = KEYEVENTF_KEYUP | KEYEVENTF_UNICODE;
    input.ki.time = 0;
    input.ki.dwExtraInfo = GetMessageExtraInfo();

    // Simulate key release
    SendInput(1, &input, sizeof(INPUT));

    return 0;
}
