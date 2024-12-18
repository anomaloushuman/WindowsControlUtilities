#include <Windows.h>
#include <iostream>
#include <cstdlib>
#include <cmath>
#include <thread>

// Function to simulate a mouse click or drag at the specified coordinates
void SimulateMouseAction(int startX, int startY, int endX, int endY, int actionType) {
    // Move the mouse cursor to the start position
    SetCursorPos(startX, startY);

    if (actionType == 0) {
        // Left-click
        mouse_event(MOUSEEVENTF_LEFTDOWN, startX, startY, 0, 0);
        mouse_event(MOUSEEVENTF_LEFTUP, startX, startY, 0, 0);
    }
    else if (actionType == 1) {
        // Right-click
        mouse_event(MOUSEEVENTF_RIGHTDOWN, startX, startY, 0, 0);
        mouse_event(MOUSEEVENTF_RIGHTUP, startX, startY, 0, 0);
    }
    else if (actionType == 2 && endX != -1 && endY != -1) {
        // Drag and hold
        mouse_event(MOUSEEVENTF_LEFTDOWN, startX, startY, 0, 0);

        // Calculate the distance and angle between start and end points
        int deltaX = endX - startX;
        int deltaY = endY - startY;
        double distance = std::sqrt(deltaX * deltaX + deltaY * deltaY);
        double angle = std::atan2(deltaY, deltaX);

        // Calculate the number of intermediate points
        int numPoints = static_cast<int>(distance);

        for (int i = 1; i <= numPoints; ++i) {
            int currentX = startX + static_cast<int>(i * distance / numPoints * std::cos(angle));
            int currentY = startY + static_cast<int>(i * distance / numPoints * std::sin(angle));

            // Move the mouse cursor smoothly
            SetCursorPos(currentX, currentY);

            // Wait for 1 millisecond
            std::this_thread::sleep_for(std::chrono::milliseconds(1));
        }

        // Release the mouse button
        mouse_event(MOUSEEVENTF_LEFTUP, endX, endY, 0, 0);
    }
}

int main(int argc, char* argv[]) {
    if (argc < 4 || argc > 6) {
        // Print usage information
        std::cout << "Usage: " << argv[0] << " <start_x> <start_y> <action_type (0/1/2)> [end_x end_y]\n";
        std::cout << "Action types:\n";
        std::cout << "  0: Left-click\n";
        std::cout << "  1: Right-click\n";
        std::cout << "  2: Drag and hold\n";
        return 1;
    }

    // Parse command-line arguments
    int startX = atoi(argv[1]);
    int startY = atoi(argv[2]);
    int actionType = atoi(argv[3]);

    if (actionType == 2 && argc != 6) {
        std::cout << "Error: For drag and hold action, both end coordinates must be provided.\n";
        return 1;
    }

    int endX = -1;
    int endY = -1;
    if (argc == 6) {
        endX = atoi(argv[4]);
        endY = atoi(argv[5]);
    }

    // Simulate the mouse action
    SimulateMouseAction(startX, startY, endX, endY, actionType);

    return 0;
}
