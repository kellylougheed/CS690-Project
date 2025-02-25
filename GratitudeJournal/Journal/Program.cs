namespace Journal;

using Spectre.Console;
using System.IO;
using System;
using System.Globalization;

class Program {
    
    static void Main(string[] args) {
        ConsoleUI ui = new ConsoleUI();
        ui.showMainMenu();
    }
}