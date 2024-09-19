using Phasmo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace phasmo
{
    public partial class journal : Form
    {
        GhostTypes ghostTypes = new GhostTypes();

        GhostData ghostData = new GhostData();

        string currentGhostSelected = null;

        private Button previouslySelectedButton = null;


        public journal()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Button previouslySelectedButton = null;
            
            foreach (Control control in ghostsGroup.Controls)
            {
                if (control is Button)
                {
                    Button btn = (Button)control;
                    int clickCount = 0;
                    btn.Click += (obj, args) =>
                    {
                        if (previouslySelectedButton != null && previouslySelectedButton != btn)
                        {
                            previouslySelectedButton.ForeColor = Color.Black;
                            clickCount = 0;
                        }

                        clickCount++;
                        switch (clickCount % 3)
                        {
                            case 0:
                                btn.ForeColor = Color.Black;
                                btn.Font = new Font(btn.Font, btn.Font.Style & ~FontStyle.Strikeout);
                                break;
                            case 1:
                                btn.ForeColor = Color.Red;
                                btn.Font = new Font(btn.Font, btn.Font.Style & ~FontStyle.Strikeout);
                                currentGhostSelected = btn.Text.ToLower();
                                updateGhostInfo();
                                break;
                            case 2:
                                btn.ForeColor = Color.Black;
                                btn.Font = new Font(btn.Font, btn.Font.Style | FontStyle.Strikeout);
                                break;
                        }

                        previouslySelectedButton = btn;
                    };
                }
            }
        }

        private void updateGhostInfo()
        {
            infoOut.Text = "";
            infoOut.MaximumSize = new Size(infoGroup.ClientSize.Width - 25, 0);

            if (currentGhostSelected == "the twins")
            {
                currentGhostSelected = "the_twins";
            }
            if (currentGhostSelected == "the mimic")
            {
                currentGhostSelected = "the_mimic";
            }

            infoOut.Text += ghostData.GhostInfo[currentGhostSelected];

            string ghostString = currentGhostSelected.ToUpper();

            Ghosts ghostEnum;

            infoOut.Text += "\n\n\n";

            if (Enum.TryParse(ghostString, out ghostEnum))
            {
                foreach (var val in ghostTypes.EvidenceMap[ghostEnum])
                {
                    infoOut.Text += val.ToString();
                    infoOut.Text += "\n";
                }
            }
        }

        private void updateState()
        {
            string ghostsString = string.Join(", ", ghostTypes.GetSortedPossibleGhosts().Select(e => e.ToString()));

            string evidenceString = string.Join(", ", ghostTypes.GetSortedEvidence().Select(e => e.ToString()));

            foreach (Control control in evidenceGroup.Controls)
            {
                if (control is Button)
                {
                    Button button = (Button)control;

                    if (!evidenceString.Contains(button.Name) && !(button.Font.Strikeout))
                    {
                        button.Enabled = false;
                    }
                    else
                    {
                        button.Enabled = true;
                    }
                }
            }

            foreach (Control control in ghostsGroup.Controls)
            {
                if (control is Button)
                {
                    Button button = (Button)control;

                    if (!ghostsString.Contains(button.Name))
                    {
                        button.Enabled = false;
                    }
                    else
                    {
                        button.Enabled = true;
                    }
                }
            }
        }

        int EMFBtnPressCtr = 0;
        private void EMF_LEVEL_5_Click(object sender, EventArgs e)
        {
            EMFBtnPressCtr++;

            if (EMFBtnPressCtr == 1)
            {
                ghostTypes.EvidenceList.Add((GhostEvidence)0);
                EMF_LEVEL_5.ForeColor = Color.Red;
                EMF_LEVEL_5.Font = new Font(EMF_LEVEL_5.Font, EMF_LEVEL_5.Font.Style & ~FontStyle.Strikeout);
            }
            else if (EMFBtnPressCtr == 2)
            {
                ghostTypes.EvidenceList.Remove((GhostEvidence)0);
                EMF_LEVEL_5.ForeColor = Color.Black;
                EMF_LEVEL_5.Font = new Font(EMF_LEVEL_5.Font, EMF_LEVEL_5.Font.Style | FontStyle.Strikeout);
                ghostTypes.StruckOutEvidenceList.Add((GhostEvidence)0);
            }
            else if (EMFBtnPressCtr == 3)
            {
                EMF_LEVEL_5.ForeColor = Color.Black;
                EMF_LEVEL_5.Font = new Font(EMF_LEVEL_5.Font, EMF_LEVEL_5.Font.Style & ~FontStyle.Strikeout);
                ghostTypes.StruckOutEvidenceList.Remove((GhostEvidence)0);
                EMFBtnPressCtr = 0;
            }

            updateState();
        }

        int ULTRAVIOLETBtnPressCtr = 0;
        private void ULTRAVIOLET_Click(object sender, EventArgs e)
        {
            ULTRAVIOLETBtnPressCtr++;

            if (ULTRAVIOLETBtnPressCtr == 1)
            {
                ghostTypes.EvidenceList.Add((GhostEvidence)1);
                ULTRAVIOLET.ForeColor = Color.Red;
                ULTRAVIOLET.Font = new Font(ULTRAVIOLET.Font, ULTRAVIOLET.Font.Style & ~FontStyle.Strikeout);
            }
            else if (ULTRAVIOLETBtnPressCtr == 2)
            {
                ghostTypes.EvidenceList.Remove((GhostEvidence)1);
                ULTRAVIOLET.ForeColor = Color.Black;
                ULTRAVIOLET.Font = new Font(ULTRAVIOLET.Font, ULTRAVIOLET.Font.Style | FontStyle.Strikeout);
                ghostTypes.StruckOutEvidenceList.Add((GhostEvidence)1);
            }
            else if (ULTRAVIOLETBtnPressCtr == 3)
            {
                ULTRAVIOLET.ForeColor = Color.Black;
                ULTRAVIOLET.Font = new Font(ULTRAVIOLET.Font, ULTRAVIOLET.Font.Style & ~FontStyle.Strikeout);
                ghostTypes.StruckOutEvidenceList.Remove((GhostEvidence)1);
                ULTRAVIOLETBtnPressCtr = 0;
            }

            updateState();
        }


        int GHOSTBtnPressCtr = 0;
        private void GHOST_WRITING_Click(object sender, EventArgs e)
        {
            GHOSTBtnPressCtr++;

            if (GHOSTBtnPressCtr == 1)
            {
                ghostTypes.EvidenceList.Add((GhostEvidence)2);
                GHOST_WRITING.ForeColor = Color.Red;
                GHOST_WRITING.Font = new Font(GHOST_WRITING.Font, GHOST_WRITING.Font.Style & ~FontStyle.Strikeout);
            }
            else if (GHOSTBtnPressCtr == 2)
            {
                ghostTypes.EvidenceList.Remove((GhostEvidence)2);
                GHOST_WRITING.ForeColor = Color.Black;
                GHOST_WRITING.Font = new Font(GHOST_WRITING.Font, GHOST_WRITING.Font.Style | FontStyle.Strikeout);
                ghostTypes.StruckOutEvidenceList.Add((GhostEvidence)2);
            }
            else if (GHOSTBtnPressCtr == 3)
            {
                GHOST_WRITING.ForeColor = Color.Black;
                GHOST_WRITING.Font = new Font(GHOST_WRITING.Font, GHOST_WRITING.Font.Style & ~FontStyle.Strikeout);
                ghostTypes.StruckOutEvidenceList.Remove((GhostEvidence)2);
                GHOSTBtnPressCtr = 0;
            }

            updateState();
        }


        int FREEZINGBtnPressCtr = 0;
        private void FREEZING_TEMPERATURES_Click(object sender, EventArgs e)
        {
            FREEZINGBtnPressCtr++;

            if (FREEZINGBtnPressCtr == 1)
            {
                ghostTypes.EvidenceList.Add((GhostEvidence)3);
                FREEZING_TEMPERATURES.ForeColor = Color.Red;
                FREEZING_TEMPERATURES.Font = new Font(FREEZING_TEMPERATURES.Font, FREEZING_TEMPERATURES.Font.Style & ~FontStyle.Strikeout);
            }
            else if (FREEZINGBtnPressCtr == 2)
            {
                ghostTypes.EvidenceList.Remove((GhostEvidence)3);
                FREEZING_TEMPERATURES.ForeColor = Color.Black;
                FREEZING_TEMPERATURES.Font = new Font(FREEZING_TEMPERATURES.Font, FREEZING_TEMPERATURES.Font.Style | FontStyle.Strikeout);
                ghostTypes.StruckOutEvidenceList.Add((GhostEvidence)3);
            }
            else if (FREEZINGBtnPressCtr == 3)
            {
                FREEZING_TEMPERATURES.ForeColor = Color.Black;
                FREEZING_TEMPERATURES.Font = new Font(FREEZING_TEMPERATURES.Font, FREEZING_TEMPERATURES.Font.Style & ~FontStyle.Strikeout);
                ghostTypes.StruckOutEvidenceList.Remove((GhostEvidence)3);
                FREEZINGBtnPressCtr = 0;
            }

            updateState();
        }


        int DOTSBtnPressCtr = 0;
        private void DOTS_PROJECTOR_Click(object sender, EventArgs e)
        {
            DOTSBtnPressCtr++;

            if (DOTSBtnPressCtr == 1)
            {
                ghostTypes.EvidenceList.Add((GhostEvidence)4);
                DOTS_PROJECTOR.ForeColor = Color.Red;
                DOTS_PROJECTOR.Font = new Font(DOTS_PROJECTOR.Font, DOTS_PROJECTOR.Font.Style & ~FontStyle.Strikeout);
            }
            else if (DOTSBtnPressCtr == 2)
            {
                ghostTypes.EvidenceList.Remove((GhostEvidence)4);
                DOTS_PROJECTOR.ForeColor = Color.Black;
                DOTS_PROJECTOR.Font = new Font(DOTS_PROJECTOR.Font, DOTS_PROJECTOR.Font.Style | FontStyle.Strikeout);
                ghostTypes.StruckOutEvidenceList.Add((GhostEvidence)4);
            }
            else if (DOTSBtnPressCtr == 3)
            {
                DOTS_PROJECTOR.ForeColor = Color.Black;
                DOTS_PROJECTOR.Font = new Font(DOTS_PROJECTOR.Font, DOTS_PROJECTOR.Font.Style & ~FontStyle.Strikeout);
                ghostTypes.StruckOutEvidenceList.Remove((GhostEvidence)4);
                DOTSBtnPressCtr = 0;
            }

            updateState();
        }


        int ORBBtnPressCtr = 0;
        private void GHOST_ORB_Click(object sender, EventArgs e)
        {
            ORBBtnPressCtr++;

            if (ORBBtnPressCtr == 1)
            {
                ghostTypes.EvidenceList.Add((GhostEvidence)5);
                GHOST_ORB.ForeColor = Color.Red;
                GHOST_ORB.Font = new Font(GHOST_ORB.Font, GHOST_ORB.Font.Style & ~FontStyle.Strikeout);
            }
            else if (ORBBtnPressCtr == 2)
            {
                ghostTypes.EvidenceList.Remove((GhostEvidence)5);
                GHOST_ORB.ForeColor = Color.Black;
                GHOST_ORB.Font = new Font(GHOST_ORB.Font, GHOST_ORB.Font.Style | FontStyle.Strikeout);
                ghostTypes.StruckOutEvidenceList.Add((GhostEvidence)5);
            }
            else if (ORBBtnPressCtr == 3)
            {
                GHOST_ORB.ForeColor = Color.Black;
                GHOST_ORB.Font = new Font(GHOST_ORB.Font, GHOST_ORB.Font.Style & ~FontStyle.Strikeout);
                ghostTypes.StruckOutEvidenceList.Remove((GhostEvidence)5);
                ORBBtnPressCtr = 0;
            }

            updateState();
        }


        int BOXBtnPressCtr = 0;
        private void SPIRIT_BOX_Click(object sender, EventArgs e)
        {
            BOXBtnPressCtr++;

            if (BOXBtnPressCtr == 1)
            {
                ghostTypes.EvidenceList.Add((GhostEvidence)6);
                SPIRIT_BOX.ForeColor = Color.Red;
                SPIRIT_BOX.Font = new Font(SPIRIT_BOX.Font, SPIRIT_BOX.Font.Style & ~FontStyle.Strikeout);
            }
            else if (BOXBtnPressCtr == 2)
            {
                ghostTypes.EvidenceList.Remove((GhostEvidence)6);
                SPIRIT_BOX.ForeColor = Color.Black;
                SPIRIT_BOX.Font = new Font(SPIRIT_BOX.Font, SPIRIT_BOX.Font.Style | FontStyle.Strikeout);
                ghostTypes.StruckOutEvidenceList.Add((GhostEvidence)6);
            }
            else if (BOXBtnPressCtr == 3)
            {
                SPIRIT_BOX.ForeColor = Color.Black;
                SPIRIT_BOX.Font = new Font(SPIRIT_BOX.Font, SPIRIT_BOX.Font.Style & ~FontStyle.Strikeout);
                ghostTypes.StruckOutEvidenceList.Remove((GhostEvidence)6);
                BOXBtnPressCtr = 0;
            }

            updateState();
        }
    }
}
