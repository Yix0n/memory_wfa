using System.Diagnostics;

namespace Memory_wfa;

public partial class Form1 : Form
{
    public int Points = 0;
    private int? _searchIndex = null;
    private Dictionary<int, Button> buttons = new Dictionary<int, Button>();
    Stopwatch timeElapsed = new Stopwatch();

    public Form1()
    {
        InitializeComponent();

        for (int i = 0; i < 16; i++)
        {
            Button btn = this.Controls.Find($"match_{i}", true).FirstOrDefault() as Button;
            if (btn != null)
            {
                buttons.Add(i, btn);
                btn.Click += Match_Clicked;
            }
        }
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        timer = new ();
        timer.Interval = 1000;
        timer.Tick += TimerTick;
        timer.Start();
    }

    private void TimerTick(object sender, EventArgs e)
    {
        var elapsed = timeElapsed.Elapsed;
        text_timer.Text = elapsed.ToString().Split('.')[0];
    }

    private void reset_Click(object sender, EventArgs e)
    {
        _searchIndex = null;
        foreach (Button button in buttons.Values) 
        { 
            button.Enabled = true;
        }
        timeElapsed.Restart();
    }

    private void Match_Clicked(object sender, EventArgs e)
    {
        Button clickedButton = sender as Button;

        if (clickedButton == null) return;

        int index = int.Parse((string)clickedButton.Tag!);

        if (_searchIndex is null)
        {
            _searchIndex = index;

            // TODO: Add Image Preview and Indicator

            return;
        }

        // TODO: Check if pair matches. If not then hide them
    }

    public void EndGame()
    {
        timeElapsed.Stop();
    }
}
