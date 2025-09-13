using System;
using System.Collections.Generic;
using Intersect.Client.Framework.Content;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Game;
using Intersect.Client.Interface.Shared;
using Intersect.Enums;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Localization;
using Intersect.Client.Core;
using Intersect.Config;
using Intersect;

public sealed class QuestRewardExp
{
    // Config visual rápida (puedes mover a JSON)
    private const int ChipW = 100;
    private const int ChipH = 40;
    private const int IconSize = 40;
    private const int IconPad = 2;

    private const string FontName = "sourcesansproblack";
    private const int FontSize = 14;

    private readonly IQuestWindow _window;
    private readonly List<ExpChip> _chips = new();

    public IEnumerable<Base> Children
    {
        get
        {
            foreach (var chip in _chips)
            {
                yield return chip.Container;

                var children = chip.Container.Children;
                if (children == null)
                {
                    continue;
                }

                foreach (var child in children)
                {
                    yield return child;
                }
            }
        }
    }

    // Mapeo EXACTO de iconos por oficio (tal como tus archivos)
    private static readonly Dictionary<JobType, string> JobIcon = new()
    {
        { JobType.Farming,     "farmingexp.png"     },
        { JobType.Mining,      "minerexp.png"       },
        { JobType.Fishing,     "fishingexp.png"     },
        { JobType.Lumberjack,  "lumberjackexp.png"  },
        { JobType.Hunter,      "huntingexp.png"     },
        { JobType.Alchemy,     "alchemyexp.png"     },
        { JobType.Smithing,    "smithingexp.png"    },
        { JobType.Cooking,     "cookingexp.png"     },
        { JobType.Crafting,    "craftingexp.png"    },
        { JobType.Jewerly,     "jewerly.png"        }, // nombre del archivo según tu carpeta
        { JobType.Tanner,      "tannerexp.png"      },
        { JobType.Tailoring,   "tailoringexp.png"   },
    };

    public QuestRewardExp(
        IQuestWindow window,
        long playerExp,
        Dictionary<JobType, long>? jobExp,
        long guildExp,
        Dictionary<Factions, int>? factionHonor
    )
    {
        _window = window;

        if (playerExp > 0)
        {
            _chips.Add(CreateChip(ExpKind.Player, playerExp));
        }

        if (jobExp != null)
        {
            foreach (var kv in jobExp)
            {
                if (kv.Value > 0)
                {
                    _chips.Add(CreateChip(ExpKind.Job, kv.Value, job: kv.Key));
                }
            }
        }

        if (guildExp > 0)
        {
            _chips.Add(CreateChip(ExpKind.Guild, guildExp));
        }

        if (factionHonor != null)
        {
            foreach (var kv in factionHonor)
            {
                if (kv.Value > 0)
                {
                    _chips.Add(CreateChip(ExpKind.Faction, kv.Value, faction: kv.Key));
                }
            }
        }
    }

    // Actualiza un tipo (útil si refrescas cantidades en caliente)
    public void Update(ExpKind kind, long newAmount, JobType? job = null, Factions? faction = null)
    {
        foreach (var c in _chips)
        {
            if (c.Kind != kind) continue;
            if (kind == ExpKind.Job && c.Job != job) continue;
            if (kind == ExpKind.Faction && c.Faction != faction) continue;

            c.Update(newAmount);
        }
    }

    private ExpChip CreateChip(ExpKind kind, long amount, JobType? job = null, Factions? faction = null)
    {
        var chip = new ExpChip(kind, amount, job, faction);

        chip.Container = new ImagePanel(null, "QuestRewardExpChip");
        chip.Container.SetSize(ChipW, ChipH);

        chip.Icon = new ImagePanel(chip.Container, "QuestRewardExpIcon")
        {
            Width = IconSize,
            Height = IconSize,
        };
        chip.Icon.SetPosition(IconPad, (ChipH - IconSize) / 2);

        chip.Label = new Label(chip.Container, "QuestRewardExpLabel")
        {
            FontName = FontName,
            FontSize = FontSize,
            Text = FormatExpText(kind, amount),
        };
        chip.Label.SetPosition(IconPad + IconSize + 6, 2);
        chip.Label.SetSize(ChipW - (IconPad + IconSize + 10), ChipH - 4);

        // Carga por JSON si lo tienes (pisa tamaños/pos)
        chip.Container.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

        // Textura del icono
        SetIconTexture(chip);

       

        // Añadir al contenedor “Exp”
        _window.AddRewardWidget(chip.Container);
        ApplyTooltip(chip, GetTooltipText(kind, job, faction));
        return chip;
    }

    private static string FormatExpText(ExpKind kind, long amount)
    {
        switch (kind)
        {
            case ExpKind.Player:
                return Strings.FormatQuantityAbbreviated(amount);
            case ExpKind.Job:
                return $"{Strings.FormatQuantityAbbreviated(amount)}";
            case ExpKind.Guild:
                return Strings.FormatQuantityAbbreviated(amount);
            case ExpKind.Faction:
                return $"{Strings.FormatQuantityAbbreviated(amount)}";
            default:
                return amount.ToString();
        }
    }

    private static void ApplyTooltip(ExpChip chip, string tooltip)
    {
        if (string.IsNullOrWhiteSpace(tooltip))
        {
            chip.Container.SetToolTipText(null);
            chip.Icon.SetToolTipText(null);
            chip.Label.SetToolTipText(null);

            return;
        }

        chip.Container.SetToolTipText(tooltip);
        chip.Icon.SetToolTipText(tooltip);
        chip.Label.SetToolTipText(tooltip);
    }

    private static string GetTooltipText(ExpKind kind, JobType? job, Factions? faction)
    {
        return kind switch
        {
            ExpKind.Player => Strings.QuestRewardExp.PlayerExpTooltip,
            ExpKind.Job when job.HasValue =>
                Strings.QuestRewardExp.JobExpTooltip.ToString(Strings.Job.GetJobName(job.Value)),
            ExpKind.Job => Strings.QuestRewardExp.JobGenericTooltip,
            ExpKind.Guild => Strings.QuestRewardExp.GuildExpTooltip,
            ExpKind.Faction when faction.HasValue => Strings.QuestRewardExp.FactionHonorTooltip
                .ToString(GetFactionDisplayName(faction.Value)),
            ExpKind.Faction => Strings.QuestRewardExp.FactionHonorGenericTooltip,
            _ => string.Empty,
        };
    }

    private static string GetFactionDisplayName(Factions faction)
    {
        return faction switch
        {
            Factions.Serolf => Strings.QuestRewardExp.FactionSerolfName,
            Factions.Nidraj => Strings.QuestRewardExp.FactionNidrajName,
            _ => Strings.QuestRewardExp.FactionNeutralName,
        };
    }

    private static void SetIconTexture(ExpChip chip)
    {
        string filename = chip.Kind switch
        {
            ExpKind.Player => FirstExisting("playerexp.png", "Expicon.png", "exp.png"),
            ExpKind.Job => ResolveJobIcon(chip.Job),
            ExpKind.Guild => FirstExisting("guildexp.png", "GuildExpicon.png"),
            ExpKind.Faction => FirstExisting("factionexp.png", "FactionExpicon.png"),
            _ => "Expicon.png"
        };

        var tex = GameContentManager.Current.GetTexture(TextureType.Misc, filename)
                  ?? GameContentManager.Current.GetTexture(TextureType.Misc, "Expicon.png");

        if (tex != null)
        {
            chip.Icon.Texture = tex;
            chip.Icon.RenderColor = new Color(255, 255, 255, 255);
        }
    }

    private static string ResolveJobIcon(JobType? job)
    {
        if (job.HasValue && JobIcon.TryGetValue(job.Value, out var file))
        {
            // Usa el archivo mapeado si existe, si no cae a Expicon.png
            if (GameContentManager.Current.GetTexture(TextureType.Misc, file) != null)
            {
                return file;
            }
        }

        // Opcional: genérico para job-exp si lo tienes; si no, usa "Expicon.png"
        return FirstExisting("jobexp.png", "Expicon.png");
    }

    private static string FirstExisting(params string[] candidates)
    {
        foreach (var name in candidates)
        {
            if (GameContentManager.Current.GetTexture(TextureType.Misc, name) != null)
            {
                return name;
            }
        }
        // último recurso
        return "Expicon.png";
    }

    private sealed class ExpChip
    {
        public ExpKind Kind { get; }
        public JobType? Job { get; }
        public Factions? Faction { get; }
        public long Amount { get; private set; }

        public ImagePanel Container { get; set; } = default!;
        public ImagePanel Icon { get; set; } = default!;
        public Label Label { get; set; } = default!;

        public ExpChip(ExpKind kind, long amount, JobType? job, Factions? faction)
        {
            Kind = kind;
            Amount = amount;
            Job = job;
            Faction = faction;
        }

        public void Update(long newAmount)
        {
            Amount = newAmount;
            Label.Text = QuestRewardExp.FormatExpText(Kind, Amount);
        }
    }

    public enum ExpKind
    {
        Player,
        Job,
        Guild,
        Faction
    }
}
