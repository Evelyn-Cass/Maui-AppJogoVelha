namespace AppJogoDaVelha
{

    public partial class MainPage : ContentPage
    {
        string turn = "X";
        string[,] board = new string[3, 3];
        public MainPage()
        {
            InitializeComponent();
        }

        private async void ButtonClicked(object sender, EventArgs e)
        {
            Button clicked = (Button)sender;

            if (clicked.Text == "X" || clicked.Text == "0")
                return;

            clicked.Text = turn;
            clicked.BackgroundColor = Color.FromArgb("#9b89b3");
            var collumn = Grid.GetColumn(clicked);
            var row = Grid.GetRow(clicked);
            board[row, collumn] = turn;
            turn = (turn == "X") ? "0" : "X";

            CheckWin();

        }

        private async void CheckWin()
        {
            string? winner = null;

            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] != null && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                    winner = board[i, 0];

                if (board[0, i] != null && board[0, i] == board[1, i] && board[1, i] == board[2, i])
                    winner = board[0, i];
            }

            if (board[0, 0] != null && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
                winner = board[0, 0];

            if (board[0, 2] != null && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
                winner = board[0, 2];

            if (winner != null)
            {
                await DisplayAlertAsync("Fim de Jogo", $"O jogador {winner} ganhou!", "OK");
                await ResetGame();
                return;
            }

            // Check for draw
            bool isDraw = true;
            foreach (var cell in board)
            {
                if (cell == null)
                {
                    isDraw = false;
                    break;
                }
            }

            if (isDraw)
            {
                await DisplayAlertAsync("Fim de Jogo", "Deu velha! Empate.", "OK");
                await ResetGame();
            }
        }

        private async Task ResetGame()
        {
            board = new string[3, 3];
            turn = "X";
            foreach (var child in game.Children)
            {
                if (child is Button button)
                {
                    button.Text = string.Empty;
                    button.BackgroundColor = Colors.Transparent;
                }
            }
        }
    }
}