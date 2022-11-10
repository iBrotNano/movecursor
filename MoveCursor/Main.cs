namespace MoveCursor
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private CancellationTokenSource cancellationTokenSource = new();
        private bool running = false;

        private void Button1_Click(object sender, EventArgs e)
        {
            if (running)
            {
                button1.Text = "Start";
                cancellationTokenSource.Cancel();
                cancellationTokenSource = new CancellationTokenSource();
                running = false;
            }
            else
            {
                button1.Text = "Stop";
                _ = MoveCursorAsync(cancellationTokenSource.Token);
                running = true;
            }
        }

        private async Task MoveCursorAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Cursor = new Cursor(Cursor.Current.Handle);

                int newX = Random.Shared.Next(1, 10) <= 5
                    ? Random.Shared.Next(1, 10)
                    : -Random.Shared.Next(1, 10);

                int newY = Random.Shared.Next(1, 10) <= 5
                    ? Random.Shared.Next(1, 10)
                    : -Random.Shared.Next(1, 10);

                Cursor.Position = new Point(Cursor.Position.X + newX, Cursor.Position.Y + newY);
                Cursor.Clip = new Rectangle(Location, Size);
                await Task.Delay(2000, cancellationToken);
            }
        }
    }
}