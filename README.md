# Battle Fleet Console Game

## Overview

Battle Fleet is a console-based implementation of the classic Battleship game in C#. This project allows two players to play Battleship against each other by taking turns entering coordinates to attack the opponent's fleet.

*In the future, we plan to add the ability to play against the computer.

## Features

- Player vs. player mode
- Player vs. computer mode
- Save and load game status
- View the history of previous matches
- View the rules of the game

## File Structure

The project is organized into the following directories:

    BattleFleet
    │
    ├── src
    │   ├── Board
    │   │   ├── Cell.cs
    │   │   ├── CellStatus.cs
    │   │   ├── Ship.cs
    │   │   ├── ShipDirection.cs
    │   │   ├── ShipClass.cs
    │   │   └── Board.cs
    │   ├── FileManager
    │   │   ├── FileManager.cs
    │   │   └── ShipTemplateManager.cs
    │   ├── Game
    │   │   ├── Game.cs
    │   │   ├── GameManager.cs
    │   │   └── IGameRules.cs
    │   ├── Player
    │   │   ├── Player.cs
    │   │   ├── HumanPlayer.cs
    │   │   └── ComputerPlayer.cs
    │   ├── UI
    │   │   └── GameConsoleUI.cs
    │   └── Tests
    │       └── (Unit test files)
    └── resources
        ├── logs
        └── saves
            └── Templates.txt


## How to Play

1. Run the application.
2. Choose from the following options:
    - **1. Start PvP game:** Begin a Player vs Player match.
        - Use ready-made templates
        - Randomize ship placement
        - Create your own ship distribution
    - **2. Start game against a computer:** Challenge the computer in a single-player match.
    - **3. Watch previous matches:** View the history of previous matches.
    - **4. Read the rules of the game:** Learn the rules of Battleship.
    - **0. Exit:** Exit the application.
3. Follow on-screen instructions to play the selected mode.

## Unit Tests

The project includes unit tests to ensure the correctness of critical components. Test classes are located in the `Tests` directory.

## How to Run

1. Clone the repository.
2. Open the solution in Visual Studio or your preferred C# IDE.
3. Build and run the application.

## Contribution Guidelines

Contributions to the project are welcome. Follow these guidelines:

1. Fork the repository.
2. Create a new branch for your feature or bug fix.
3. Make your changes and submit a pull request.
4. Ensure your code passes the existing unit tests.

## License

This project is licensed under the MIT License - [MIT License](LICENSE.txt).


