using System;

class Program {
  static bool exitFlag = false;

  static void Main() {
    Console.CancelKeyPress += new ConsoleCancelEventHandler(OnCancelKeyPress);

    while (!exitFlag) {
      // Perform program tasks
    }
  }

  static void OnCancelKeyPress(object sender, ConsoleCancelEventArgs e) {
    e.Cancel = true;
    exitFlag = true;
  }
}