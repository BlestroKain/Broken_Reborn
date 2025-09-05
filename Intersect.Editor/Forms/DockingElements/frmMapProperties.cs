using System;
using System.Linq;
using System.Windows.Forms;
using Intersect.Editor.Core;
using Intersect.Editor.Localization;
using Intersect.Editor.Maps;
using Intersect.Editor.Networking;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Zones;
using WeifenLuo.WinFormsUI.Docking;

namespace Intersect.Editor.Forms.DockingElements;


public partial class FrmMapProperties : DockContent
{

    //Cross Thread Delegates
    public delegate void UpdateProperties();

    public UpdateProperties UpdatePropertiesDelegate;

    private MapProperties mProperties;

    public FrmMapProperties()
    {
        InitializeComponent();
        Icon = Program.Icon;

        UpdatePropertiesDelegate = Update;

        cmbArea.SelectedIndexChanged += CmbArea_SelectedIndexChanged;
        cmbSubarea.SelectedIndexChanged += CmbSubarea_SelectedIndexChanged;
        btnEditZones.Click += BtnEditZones_Click;
    }

    public void Init(MapInstance map)
    {
        if (gridMapProperties.InvokeRequired)
        {
            gridMapProperties.Invoke((MethodInvoker) delegate { Init(map); });

            return;
        }

        mProperties = new MapProperties(map);
        gridMapProperties.SelectedObject = mProperties;
        InitLocalization();
        LoadZones();
    }

    private void InitLocalization()
    {
        Text = Strings.MapProperties.title;
    }

    private void LoadZones()
    {
        cmbArea.SelectedIndexChanged -= CmbArea_SelectedIndexChanged;
        cmbSubarea.SelectedIndexChanged -= CmbSubarea_SelectedIndexChanged;

        var zones = Zone.Lookup.OrderBy(z => z.Value?.Name).Select(z => z.Value).ToList();
        zones.Insert(0, new Zone(Guid.Empty) { Name = Strings.General.None });
        cmbArea.DisplayMember = nameof(Zone.Name);
        cmbArea.ValueMember = nameof(Zone.Id);
        cmbArea.DataSource = zones;

        if (mProperties?.ZoneId != null)
        {
            cmbArea.SelectedValue = mProperties.ZoneId;
        }
        else
        {
            cmbArea.SelectedIndex = 0;
        }

        LoadSubzones();

        cmbArea.SelectedIndexChanged += CmbArea_SelectedIndexChanged;
        cmbSubarea.SelectedIndexChanged += CmbSubarea_SelectedIndexChanged;
    }

    private void LoadSubzones()
    {
        cmbSubarea.SelectedIndexChanged -= CmbSubarea_SelectedIndexChanged;

        var zoneId = mProperties?.ZoneId ?? Guid.Empty;
        var subzones = Subzone.Lookup.Where(s => s.Value?.ZoneId == zoneId)
            .OrderBy(s => s.Value?.Name)
            .Select(s => s.Value)
            .ToList();
        subzones.Insert(0, new Subzone(Guid.Empty) { Name = Strings.General.None, ZoneId = zoneId });
        cmbSubarea.DisplayMember = nameof(Subzone.Name);
        cmbSubarea.ValueMember = nameof(Subzone.Id);
        cmbSubarea.DataSource = subzones;

        if (mProperties?.SubzoneId != null)
        {
            cmbSubarea.SelectedValue = mProperties.SubzoneId;
        }
        else
        {
            cmbSubarea.SelectedIndex = 0;
        }

        cmbSubarea.SelectedIndexChanged += CmbSubarea_SelectedIndexChanged;
    }

    private void CmbArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbArea.SelectedItem is not Zone zone || mProperties == null)
        {
            return;
        }

        mProperties.ZoneId = zone.Id == Guid.Empty ? null : zone.Id;
        LoadSubzones();
    }

    private void CmbSubarea_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbSubarea.SelectedItem is not Subzone sub || mProperties == null)
        {
            return;
        }

        mProperties.SubzoneId = sub.Id == Guid.Empty ? null : sub.Id;
    }

    private void BtnEditZones_Click(object sender, EventArgs e)
    {
        PacketSender.SendOpenEditor(GameObjectType.Zone);
    }

    public void Update()
    {
        gridMapProperties.Refresh();
    }

    public GridItem Selection()
    {
        return gridMapProperties.SelectedGridItem;
    }

}
