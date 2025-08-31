using System;
using Intersect.Client.Core;
using Intersect.Client.Entities;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.ControlInternal;
using Intersect.Client.General;
using Intersect.Client.Networking;
using Intersect.Enums;

namespace Intersect.Client.Interface.Game;

public sealed class FactionWindow : Window
{
    // Layout base
    private const int W = 300;
    private const int H = 170;
    private const int PAD = 12;

    // Alturas de fila aproximadas
    private const int RowHSmall = 18;
    private const int RowHMedium = 20;
    private const int BtnH = 26;

    // UI
    private readonly Label _title;
    private readonly Label _factionLabel;
    private readonly Label _honorLabel;
    private readonly Label _gradeLabel;
    private readonly Label _cooldownLabel;
    private readonly Button _wingsButton;
    // Honor bar
    private readonly Base _honorBarBg;
    private readonly Base _honorBarMid;
    private readonly Base _honorBarFill;

    // Honor bounds
    private const int HonorMin = -5000;
    private const int HonorMax = +5000;

    public FactionWindow(Canvas gameCanvas)
        : base(gameCanvas, nameof(FactionWindow), false, nameof(FactionWindow))
    {
        IsResizable = false;
        SetSize(W, H);


        Padding = new Padding(PAD, PAD, PAD, PAD);
        Margin = new Margin(0, 0, 0, 0);
        Title = "Facción";

        // contenedor
        var content = new Base(this, "FactionContent");
        content.SetPosition(PAD, PAD);
        content.SetSize(W - PAD * 2, H - PAD * 2);

        // acumulador de Y para layout absoluto
        var y = 0;

        // Título
        _title = new Label(content, "TitleLabel")
        {
            Text = "Estado de Alineación",
            TextColor = Color.White
        };
        _title.SetPosition(0, y);
        _title.SetSize(content.Width, RowHMedium);
        y += RowHMedium + 4;

        // Facción
        _factionLabel = new Label(content, "FactionLabel")
        {
            Text = "Neutral",
            TextColor = Color.FromArgb(230, 230, 230)
        };
        _factionLabel.SetPosition(0, y);
        _factionLabel.SetSize(content.Width, RowHMedium);
        y += RowHMedium + 4;

        // Honor bar bg
        _honorBarBg = new Base(content, "HonorBarBg")
        {
            ShouldDrawBackground = true,
    
        };
        _honorBarBg.Padding = new Padding(1);
        _honorBarBg.SetPosition(0, y);
        _honorBarBg.SetSize(content.Width, RowHSmall); // 18px alto
        y += RowHSmall + 4;

        // Línea central (cero) dentro del bg
        _honorBarMid = new Base(_honorBarBg, "HonorBarMid")
        {
            ShouldDrawBackground = true,
  
        };
        _honorBarMid.SetSize(2, RowHSmall - 2);
        _honorBarMid.SetPosition((_honorBarBg.Width / 2) - 1, 1);

        // Relleno (se ajusta en LayoutHonorBar)
        _honorBarFill = new Base(_honorBarBg, "HonorBarFill")
        {
            ShouldDrawBackground = true,

        };
        _honorBarFill.SetSize(0, RowHSmall - 2);
        _honorBarFill.SetPosition(_honorBarBg.Width / 2, 1);

        // Honor text
        _honorLabel = new Label(content, "HonorLabel")
        {
            Text = "Honor: 0",
            TextColor = Color.FromArgb(210, 210, 210)
        };
        _honorLabel.SetPosition(0, y);
        _honorLabel.SetSize(content.Width, RowHMedium);
        y += RowHMedium + 2;

        // Grade
        _gradeLabel = new Label(content, "GradeLabel")
        {
            Text = "Grado: —",
            TextColor = Color.FromArgb(210, 210, 210)
        };
        _gradeLabel.SetPosition(0, y);
        _gradeLabel.SetSize(content.Width, RowHMedium);
        y += RowHMedium + 4;

        _cooldownLabel = new Label(content, "CooldownLabel")
        {
            Text = string.Empty,
            TextColor = Color.FromArgb(210, 210, 210)
        };
        _cooldownLabel.SetPosition(0, y);
        _cooldownLabel.SetSize(content.Width, RowHMedium);
        y += RowHMedium + 6;

        // Botón Wings
        _wingsButton = new Button(content, "WingsButton");
        _wingsButton.SetText("Alas: OFF");
        _wingsButton.SetPosition(0, y);
        _wingsButton.SetSize(110, BtnH);
        _wingsButton.Clicked += (_, __) =>
        {
            var me = Globals.Me;
            if (me is null) return;

            var newState = me.Wings == WingState.On ? WingState.Off : WingState.On;
            me.Wings = newState;
            UpdateWingsButton(newState);
            PacketSender.SendToggleWings(newState);
        };
        // primera actualización
        Refresh();
    }

    public void Refresh()
    {
        var me = Globals.Me;
        if (me == null) return;

        var (factionName, accent) = me.Faction switch
        {
            Factions.Serolf => ("Serolf", Color.FromArgb(120, 180, 255)),
            Factions.Nidraj => ("Nidraj", Color.FromArgb(255, 120, 120)),
            _ => ("Neutral", Color.FromArgb(190, 190, 190))
        };

        _factionLabel.Text = $"Facción: {factionName}";
        _factionLabel.TextColor = accent;
        _title.TextColor = accent;

        _honorLabel.Text = $"Honor: {me.Honor}";
        _honorLabel.TextColor = me.Honor >= 0
            ? Color.FromArgb(160, 210, 255)
            : Color.FromArgb(255, 160, 160);

        var grade = (FactionGrade)Math.Clamp(me.Grade, 0, (int)FactionGrade.Legend);
        _gradeLabel.Text = $"Grado: {grade}";

        if (me.NextFactionChangeAt.HasValue)
        {
            _cooldownLabel.Text = $"Próximo cambio: {me.NextFactionChangeAt:dd/MM HH:mm}";
            _cooldownLabel.Show();
        }
        else
        {
            _cooldownLabel.Hide();
        }

        LayoutHonorBar(me.Honor);
        UpdateWingsButton(me.Wings);
    }

    private void LayoutHonorBar(int honor)
    {
        // actualizar mid al centro (por si JSON cambió tamaños)
        _honorBarMid.SetPosition((_honorBarBg.Width / 2) - 1, 1);
        _honorBarMid.SetSize(2, _honorBarBg.Height - 2);

        var innerH = _honorBarBg.Height - 2;
        var innerW = _honorBarBg.Width - 2;

        var clamped = Math.Clamp(honor, HonorMin, HonorMax);
        var ratio = Math.Abs(clamped) / (float)HonorMax;
        var halfW = innerW / 2f;
        var fillW = (int)Math.Round(halfW * ratio);

        if (clamped >= 0)
        {
           
            _honorBarFill.SetBounds((int)(halfW) + 1, 1, Math.Max(0, fillW), innerH);
        }
        else
        {

            _honorBarFill.SetBounds((int)(halfW) + 1 - fillW, 1, Math.Max(0, fillW), innerH);
        }
    }

    private void UpdateWingsButton(WingState state)
    {
        var on = state == WingState.On;
        _wingsButton.Text = on ? "Alas: ON" : "Alas: OFF";
        _wingsButton.TextColor = on ? Color.FromArgb(70, 255, 140) : Color.FromArgb(220, 220, 220);
        _wingsButton.ShouldDrawBackground = true;
 
    }

    protected override void EnsureInitialized()
    {
        LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
    }
}
