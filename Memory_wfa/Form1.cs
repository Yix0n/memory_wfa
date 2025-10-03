using System.Diagnostics;

namespace Memory_wfa;

public partial class Form1 : Form
{
    public int Points = 0;
    private int? _searchIndex = null;
    private int? _openedIndex = null;
    private Dictionary<int, Button> buttons = new Dictionary<int, Button>();
    Stopwatch timeElapsed = new Stopwatch();
    public bool showing = false;
    private int _show = 0;
    private static int ShowTimer = 1;

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
        timer = new();
        timer.Interval = 1000;
        timer.Tick += TimerTick;
        timer.Start();
    }

    private void TimerTick(object sender, EventArgs e)
    {
        var elapsed = timeElapsed.Elapsed;
        text_timer.Text = elapsed.ToString().Split('.')[0];

        if (showing)
        {
            if (_show != ShowTimer)
            {
                _show++;
                return;
            };
            var searchButton = buttons[_searchIndex.Value];
            var openedButtons = buttons[_openedIndex.Value];
            
            searchButton.Image = Properties.Resources.unknown;
            searchButton.FlatStyle = FlatStyle.Standard;
            searchButton.FlatAppearance.BorderColor = Color.White;
            
            openedButtons.Image = Properties.Resources.unknown;
            openedButtons.FlatStyle = FlatStyle.Standard;
            openedButtons.FlatAppearance.BorderColor = Color.White;
            
            showing = false;
            _searchIndex = null;
            _openedIndex = null;
            _show = 0;
            
        }
    }

    private void reset_Click(object sender, EventArgs e)
    {
        _searchIndex = null;
        foreach (Button button in buttons.Values)
        {
            button.Enabled = true;
        }
        
        Memory.GenerateDeck();
        
        timeElapsed.Restart();
    }

    private void Match_Clicked(object sender, EventArgs e)
    {
        if (showing) return;
        
        Button clickedButton = sender as Button;

        if (clickedButton == null) return;

        int index;
        var tag = clickedButton.Tag;

        Match selected = null;

        if (int.TryParse(tag.ToString(), out index))
        {
            selected = Memory.GetMatch(index);
        }

        clickedButton.Image = selected.Image;
        clickedButton.FlatStyle = FlatStyle.Flat;
        clickedButton.FlatAppearance.BorderColor = Color.Yellow;
        
        if (_searchIndex is null)
        {
            _searchIndex = index;
        }

        if (_searchIndex == index) return;
        
        Match match1 = Memory.GetMatch(index);
        Match match2 = Memory.GetMatch(_searchIndex.Value);

        if (match1.CanMatch(match2))
        {
            Points += 5;
            text_points.Text = Points.ToString();
            clickedButton.Enabled = false;
            buttons[_searchIndex.Value].Enabled = false;
            _searchIndex = null;
            _openedIndex = null;
            // TOOD: Can Match. Add point and update

            if (Points == 40)
            {
                timeElapsed.Stop();
                MessageBox.Show("Koniec Gry");
            }
        }
        else
        {
            _openedIndex = index;
            showing = true;
        }
    }

    public void EndGame()
    {
        timeElapsed.Stop();
    }
}
