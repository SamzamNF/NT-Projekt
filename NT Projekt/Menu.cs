using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NT_Projekt {
    public class Menu {
        //Private felter
        private int _selectedIndex;
        private string[] _options;
        private string _prompt;

        //Constructor med parametre string prompt og string[] options
        public Menu(string prompt, string[] options) {
            _prompt = prompt;
            _options = options;
            _selectedIndex = 0;
        }

        //Privat metode til at vise hvordan det valgte element ser ud i forhold til de andre elementer
        private void DisplayOptions() {
            Console.WriteLine(_prompt);
            for (int i = 0; i < _options.Length; i++) {
                string currentOption = _options[i];

                if (i == _selectedIndex) {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.WriteLine($"<< {currentOption} >>");
            }
            Console.ResetColor();
        }

        /*
         * Håndtere bruger-input i en menu
         * Lader brugeren navigere med piltasterne
         * Når brugeren trykker Enter, returneres det vaglte indeks
         * Implementere Wrap Around logik, hvis brugeren går over grænsen hopper den fra f.eks. sidste element til det første
         */
        public int Run() {
            ConsoleKey keyPressed;

            do {
                Console.Clear();
                DisplayOptions();
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow) {
                    _selectedIndex--;
                    if (_selectedIndex == -1) {
                        _selectedIndex = _options.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow) {
                    _selectedIndex++;
                    if (_selectedIndex == _options.Length) {
                        _selectedIndex = 0;
                    }
                }
            } while (keyPressed != ConsoleKey.Enter);

            return _selectedIndex;
        }
    }
}

/*
public class Menu {
        private int _selectedIndex;
        private readonly string[] _options;
        private readonly string _prompt;

        public Menu(string prompt, string[] options) {
            _prompt = prompt;
            _options = options;
            _selectedIndex = 0;
        }

        public int Run() {
            Console.Clear();
            Console.WriteLine(_prompt);

            for (int i = 0; i < _options.Length; i++) {
                Console.SetCursorPosition(0, i + 2);
                Console.WriteLine(_options[i]);
            }

            while (true) {
                Console.SetCursorPosition(0, _selectedIndex + 2);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write(_options[_selectedIndex]);
                Console.ResetColor();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                Console.SetCursorPosition(0, _selectedIndex + 2);
                Console.Write(_options[_selectedIndex]);

                if (keyInfo.Key == ConsoleKey.UpArrow) {
                    _selectedIndex = (_selectedIndex - 1 + _options.Length) % _options.Length;
                } else if (keyInfo.Key == ConsoleKey.DownArrow) {
                    _selectedIndex = (_selectedIndex + 1) % _options.Length;
                } else if (keyInfo.Key == ConsoleKey.Enter) {
                    break;
                }
            }

            return _selectedIndex;
        }
    }
}
*/
