﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect;
using Coding4Fun.Kinect.Wpf;
using System.Timers;
using System.Collections;

namespace KinectToolkit
{

    public partial class MainWindow : Window
    {
        public struct actionarray
        {
            public SkeletonPoint head;
            public SkeletonPoint neck;
        }
        actionarray[] stand_data = new actionarray[60];
        private void Set_stand_data()
        {
            actionarray stand_flip;
            {
                stand_flip.head.X = 0.02067427F;
                stand_flip.head.Y = 0.5404437F;
                stand_flip.head.Z = -0.03752136F;
                stand_flip.neck.X = 0.005116371F;
                stand_flip.neck.Y = 0.3483205F;
                stand_flip.neck.Z = -0.009822488F;
                stand_data[0] = stand_flip;
                stand_flip.head.X = 0.02069657F;
                stand_flip.head.Y = 0.5404443F;
                stand_flip.head.Z = -0.03747678F;
                stand_flip.neck.X = 0.00512894F;
                stand_flip.neck.Y = 0.3483155F;
                stand_flip.neck.Z = -0.00982058F;
                stand_data[1] = stand_flip;
                stand_flip.head.X = 0.02072134F;
                stand_flip.head.Y = 0.540444F;
                stand_flip.head.Z = -0.03743124F;
                stand_flip.neck.X = 0.005144778F;
                stand_flip.neck.Y = 0.3483101F;
                stand_flip.neck.Z = -0.009817839F;
                stand_data[2] = stand_flip;
                stand_flip.head.X = 0.02075659F;
                stand_flip.head.Y = 0.5404408F;
                stand_flip.head.Z = -0.03737819F;
                stand_flip.neck.X = 0.005165907F;
                stand_flip.neck.Y = 0.3483047F;
                stand_flip.neck.Z = -0.009813666F;
                stand_data[3] = stand_flip;
                stand_flip.head.X = 0.02081523F;
                stand_flip.head.Y = 0.5404381F;
                stand_flip.head.Z = -0.03730202F;
                stand_flip.neck.X = 0.005191476F;
                stand_flip.neck.Y = 0.3482988F;
                stand_flip.neck.Z = -0.009807706F;
                stand_data[4] = stand_flip;
                stand_flip.head.X = 0.02087425F;
                stand_flip.head.Y = 0.5404348F;
                stand_flip.head.Z = -0.03722644F;
                stand_flip.neck.X = 0.005219016F;
                stand_flip.neck.Y = 0.3482932F;
                stand_flip.neck.Z = -0.009801984F;
                stand_data[5] = stand_flip;
                stand_flip.head.X = 0.02094286F;
                stand_flip.head.Y = 0.5404311F;
                stand_flip.head.Z = -0.03714633F;
                stand_flip.neck.X = 0.00524769F;
                stand_flip.neck.Y = 0.3482873F;
                stand_flip.neck.Z = -0.009796977F;
                stand_data[6] = stand_flip;
                stand_flip.head.X = 0.02101506F;
                stand_flip.head.Y = 0.5404264F;
                stand_flip.head.Z = -0.03706455F;
                stand_flip.neck.X = 0.005281738F;
                stand_flip.neck.Y = 0.348282F;
                stand_flip.neck.Z = -0.009793282F;
                stand_data[7] = stand_flip;
                stand_flip.head.X = 0.02108918F;
                stand_flip.head.Y = 0.5404214F;
                stand_flip.head.Z = -0.03698492F;
                stand_flip.neck.X = 0.00531816F;
                stand_flip.neck.Y = 0.3482776F;
                stand_flip.neck.Z = -0.009789586F;
                stand_data[8] = stand_flip;
                stand_flip.head.X = 0.02116115F;
                stand_flip.head.Y = 0.5404155F;
                stand_flip.head.Z = -0.03690875F;
                stand_flip.neck.X = 0.005356886F;
                stand_flip.neck.Y = 0.3482735F;
                stand_flip.neck.Z = -0.009785891F;
                stand_data[9] = stand_flip;
                stand_flip.head.X = 0.02123027F;
                stand_flip.head.Y = 0.5404088F;
                stand_flip.head.Z = -0.03683889F;
                stand_flip.neck.X = 0.005395165F;
                stand_flip.neck.Y = 0.348269F;
                stand_flip.neck.Z = -0.009783626F;
                stand_data[10] = stand_flip;
                stand_flip.head.X = 0.02129133F;
                stand_flip.head.Y = 0.5404006F;
                stand_flip.head.Z = -0.03678191F;
                stand_flip.neck.X = 0.005436228F;
                stand_flip.neck.Y = 0.3482614F;
                stand_flip.neck.Z = -0.009783387F;
                stand_data[11] = stand_flip;
                stand_flip.head.X = 0.02134914F;
                stand_flip.head.Y = 0.5403902F;
                stand_flip.head.Z = -0.03673267F;
                stand_flip.neck.X = 0.00547611F;
                stand_flip.neck.Y = 0.3482533F;
                stand_flip.neck.Z = -0.009783864F;
                stand_data[12] = stand_flip;
                stand_flip.head.X = 0.02139647F;
                stand_flip.head.Y = 0.5403802F;
                stand_flip.head.Z = -0.0366956F;
                stand_flip.neck.X = 0.005512406F;
                stand_flip.neck.Y = 0.3482453F;
                stand_flip.neck.Z = -0.009786606F;
                stand_data[13] = stand_flip;
                stand_flip.head.X = 0.02143854F;
                stand_flip.head.Y = 0.5403684F;
                stand_flip.head.Z = -0.03666592F;
                stand_flip.neck.X = 0.005546972F;
                stand_flip.neck.Y = 0.3482366F;
                stand_flip.neck.Z = -0.009792924F;
                stand_data[14] = stand_flip;
                stand_flip.head.X = 0.021475F;
                stand_flip.head.Y = 0.5403563F;
                stand_flip.head.Z = -0.03664076F;
                stand_flip.neck.X = 0.005580751F;
                stand_flip.neck.Y = 0.3482267F;
                stand_flip.neck.Z = -0.009801865F;
                stand_data[15] = stand_flip;
                stand_flip.head.X = 0.02150511F;
                stand_flip.head.Y = 0.5403432F;
                stand_flip.head.Z = -0.03662026F;
                stand_flip.neck.X = 0.005609771F;
                stand_flip.neck.Y = 0.3482164F;
                stand_flip.neck.Z = -0.009814501F;
                stand_data[16] = stand_flip;
                stand_flip.head.X = 0.0215311F;
                stand_flip.head.Y = 0.5403299F;
                stand_flip.head.Z = -0.03660202F;
                stand_flip.neck.X = 0.00563705F;
                stand_flip.neck.Y = 0.3482065F;
                stand_flip.neck.Z = -0.009827256F;
                stand_data[17] = stand_flip;
                stand_flip.head.X = 0.02154949F;
                stand_flip.head.Y = 0.5403175F;
                stand_flip.head.Z = -0.03658867F;
                stand_flip.neck.X = 0.005663402F;
                stand_flip.neck.Y = 0.3481965F;
                stand_flip.neck.Z = -0.009840608F;
                stand_data[18] = stand_flip;
                stand_flip.head.X = 0.02156072F;
                stand_flip.head.Y = 0.5402816F;
                stand_flip.head.Z = -0.03659141F;
                stand_flip.neck.X = 0.005687436F;
                stand_flip.neck.Y = 0.3481838F;
                stand_flip.neck.Z = -0.00985539F;
                stand_data[19] = stand_flip;
                stand_flip.head.X = 0.02156908F;
                stand_flip.head.Y = 0.5402375F;
                stand_flip.head.Z = -0.03660595F;
                stand_flip.neck.X = 0.005709654F;
                stand_flip.neck.Y = 0.3481715F;
                stand_flip.neck.Z = -0.009868503F;
                stand_data[20] = stand_flip;
                stand_flip.head.X = 0.0215761F;
                stand_flip.head.Y = 0.5401905F;
                stand_flip.head.Z = -0.03662336F;
                stand_flip.neck.X = 0.005730845F;
                stand_flip.neck.Y = 0.3481583F;
                stand_flip.neck.Z = -0.009881854F;
                stand_data[21] = stand_flip;
                stand_flip.head.X = 0.02158226F;
                stand_flip.head.Y = 0.54014F;
                stand_flip.head.Z = -0.03664577F;
                stand_flip.neck.X = 0.005749796F;
                stand_flip.neck.Y = 0.3481453F;
                stand_flip.neck.Z = -0.00989449F;
                stand_data[22] = stand_flip;
                stand_flip.head.X = 0.02158789F;
                stand_flip.head.Y = 0.5400534F;
                stand_flip.head.Z = -0.03670406F;
                stand_flip.neck.X = 0.005767748F;
                stand_flip.neck.Y = 0.3481315F;
                stand_flip.neck.Z = -0.009905338F;
                stand_data[23] = stand_flip;
                stand_flip.head.X = 0.02159577F;
                stand_flip.head.Y = 0.5399702F;
                stand_flip.head.Z = -0.03676641F;
                stand_flip.neck.X = 0.00578418F;
                stand_flip.neck.Y = 0.3481183F;
                stand_flip.neck.Z = -0.009915471F;
                stand_data[24] = stand_flip;
                stand_flip.head.X = 0.02160304F;
                stand_flip.head.Y = 0.5398923F;
                stand_flip.head.Z = -0.03682804F;
                stand_flip.neck.X = 0.005799758F;
                stand_flip.neck.Y = 0.3481062F;
                stand_flip.neck.Z = -0.0099262F;
                stand_data[25] = stand_flip;
                stand_flip.head.X = 0.02161889F;
                stand_flip.head.Y = 0.5398232F;
                stand_flip.head.Z = -0.036888F;
                stand_flip.neck.X = 0.005813641F;
                stand_flip.neck.Y = 0.3480949F;
                stand_flip.neck.Z = -0.009936094F;
                stand_data[26] = stand_flip;
                stand_flip.head.X = 0.02164203F;
                stand_flip.head.Y = 0.5397609F;
                stand_flip.head.Z = -0.03694606F;
                stand_flip.neck.X = 0.005827705F;
                stand_flip.neck.Y = 0.3480842F;
                stand_flip.neck.Z = -0.009946585F;
                stand_data[27] = stand_flip;
                stand_flip.head.X = 0.02168605F;
                stand_flip.head.Y = 0.5397121F;
                stand_flip.head.Z = -0.03701866F;
                stand_flip.neck.X = 0.005840429F;
                stand_flip.neck.Y = 0.348074F;
                stand_flip.neck.Z = -0.00995791F;
                stand_data[28] = stand_flip;
                stand_flip.head.X = 0.02172971F;
                stand_flip.head.Y = 0.5396708F;
                stand_flip.head.Z = -0.03709114F;
                stand_flip.neck.X = 0.005852425F;
                stand_flip.neck.Y = 0.3480648F;
                stand_flip.neck.Z = -0.009969234F;
                stand_data[29] = stand_flip;
                stand_flip.head.X = 0.02178006F;
                stand_flip.head.Y = 0.5396355F;
                stand_flip.head.Z = -0.03715909F;
                stand_flip.neck.X = 0.005863183F;
                stand_flip.neck.Y = 0.3480566F;
                stand_flip.neck.Z = -0.009981513F;
                stand_data[30] = stand_flip;
                stand_flip.head.X = 0.02183041F;
                stand_flip.head.Y = 0.5396062F;
                stand_flip.head.Z = -0.03722262F;
                stand_flip.neck.X = 0.005873251F;
                stand_flip.neck.Y = 0.3480499F;
                stand_flip.neck.Z = -0.009993196F;
                stand_data[31] = stand_flip;
                stand_flip.head.X = 0.02188966F;
                stand_flip.head.Y = 0.5395814F;
                stand_flip.head.Z = -0.03729904F;
                stand_flip.neck.X = 0.005882838F;
                stand_flip.neck.Y = 0.3480429F;
                stand_flip.neck.Z = -0.01000726F;
                stand_data[32] = stand_flip;
                stand_flip.head.X = 0.02194665F;
                stand_flip.head.Y = 0.539562F;
                stand_flip.head.Z = -0.03737426F;
                stand_flip.neck.X = 0.005892577F;
                stand_flip.neck.Y = 0.3480363F;
                stand_flip.neck.Z = -0.01001906F;
                stand_data[33] = stand_flip;
                stand_flip.head.X = 0.02199636F;
                stand_flip.head.Y = 0.5395476F;
                stand_flip.head.Z = -0.03744388F;
                stand_flip.neck.X = 0.005902817F;
                stand_flip.neck.Y = 0.3480304F;
                stand_flip.neck.Z = -0.01003253F;
                stand_data[34] = stand_flip;
                stand_flip.head.X = 0.02202399F;
                stand_flip.head.Y = 0.5395177F;
                stand_flip.head.Z = -0.03753269F;
                stand_flip.neck.X = 0.005913089F;
                stand_flip.neck.Y = 0.3480232F;
                stand_flip.neck.Z = -0.01004672F;
                stand_data[35] = stand_flip;
                stand_flip.head.X = 0.02204409F;
                stand_flip.head.Y = 0.5394922F;
                stand_flip.head.Z = -0.03761613F;
                stand_flip.neck.X = 0.005925745F;
                stand_flip.neck.Y = 0.3480153F;
                stand_flip.neck.Z = -0.01006639F;
                stand_data[36] = stand_flip;
                stand_flip.head.X = 0.02205986F;
                stand_flip.head.Y = 0.5394703F;
                stand_flip.head.Z = -0.03769326F;
                stand_flip.neck.X = 0.005941835F;
                stand_flip.neck.Y = 0.3480072F;
                stand_flip.neck.Z = -0.0100888F;
                stand_data[37] = stand_flip;
                stand_flip.head.X = 0.02207499F;
                stand_flip.head.Y = 0.5394553F;
                stand_flip.head.Z = -0.03776264F;
                stand_flip.neck.X = 0.005958531F;
                stand_flip.neck.Y = 0.3479992F;
                stand_flip.neck.Z = -0.01011252F;
                stand_data[38] = stand_flip;
                stand_flip.head.X = 0.02208789F;
                stand_flip.head.Y = 0.539442F;
                stand_flip.head.Z = -0.0378238F;
                stand_flip.neck.X = 0.005975553F;
                stand_flip.neck.Y = 0.347991F;
                stand_flip.neck.Z = -0.01013517F;
                stand_data[39] = stand_flip;
                stand_flip.head.X = 0.02210017F;
                stand_flip.head.Y = 0.5394323F;
                stand_flip.head.Z = -0.03787744F;
                stand_flip.neck.X = 0.005991844F;
                stand_flip.neck.Y = 0.3479837F;
                stand_flip.neck.Z = -0.01015675F;
                stand_data[40] = stand_flip;
                stand_flip.head.X = 0.02211184F;
                stand_flip.head.Y = 0.5394244F;
                stand_flip.head.Z = -0.03792346F;
                stand_flip.neck.X = 0.00600815F;
                stand_flip.neck.Y = 0.3479776F;
                stand_flip.neck.Z = -0.01017666F;
                stand_data[41] = stand_flip;
                stand_flip.head.X = 0.0221211F;
                stand_flip.head.Y = 0.5394191F;
                stand_flip.head.Z = -0.03796208F;
                stand_flip.neck.X = 0.006023428F;
                stand_flip.neck.Y = 0.3479723F;
                stand_flip.neck.Z = -0.0101949F;
                stand_data[42] = stand_flip;
                stand_flip.head.X = 0.0221292F;
                stand_flip.head.Y = 0.539413F;
                stand_flip.head.Z = -0.03799665F;
                stand_flip.neck.X = 0.006035704F;
                stand_flip.neck.Y = 0.3479671F;
                stand_flip.neck.Z = -0.01021016F;
                stand_data[43] = stand_flip;
                stand_flip.head.X = 0.02213606F;
                stand_flip.head.Y = 0.5394087F;
                stand_flip.head.Z = -0.03802562F;
                stand_flip.neck.X = 0.006047242F;
                stand_flip.neck.Y = 0.3479628F;
                stand_flip.neck.Z = -0.01022279F;
                stand_data[44] = stand_flip;
                stand_flip.head.X = 0.022142F;
                stand_flip.head.Y = 0.5394059F;
                stand_flip.head.Z = -0.03805375F;
                stand_flip.neck.X = 0.006056048F;
                stand_flip.neck.Y = 0.3479595F;
                stand_flip.neck.Z = -0.01023293F;
                stand_data[45] = stand_flip;
                stand_flip.head.X = 0.02214715F;
                stand_flip.head.Y = 0.5394049F;
                stand_flip.head.Z = -0.03807831F;
                stand_flip.neck.X = 0.00606316F;
                stand_flip.neck.Y = 0.3479573F;
                stand_flip.neck.Z = -0.01024151F;
                stand_data[46] = stand_flip;
                stand_flip.head.X = 0.02215218F;
                stand_flip.head.Y = 0.5394048F;
                stand_flip.head.Z = -0.03809702F;
                stand_flip.neck.X = 0.006068604F;
                stand_flip.neck.Y = 0.3479556F;
                stand_flip.neck.Z = -0.0102483F;
                stand_data[47] = stand_flip;
                stand_flip.head.X = 0.02215542F;
                stand_flip.head.Y = 0.5394053F;
                stand_flip.head.Z = -0.03811252F;
                stand_flip.neck.X = 0.006073743F;
                stand_flip.neck.Y = 0.3479546F;
                stand_flip.neck.Z = -0.01025236F;
                stand_data[48] = stand_flip;
                stand_flip.head.X = 0.02215725F;
                stand_flip.head.Y = 0.5394021F;
                stand_flip.head.Z = -0.03813362F;
                stand_flip.neck.X = 0.00607908F;
                stand_flip.neck.Y = 0.3479531F;
                stand_flip.neck.Z = -0.01025414F;
                stand_data[49] = stand_flip;
                stand_flip.head.X = 0.02215657F;
                stand_flip.head.Y = 0.5393974F;
                stand_flip.head.Z = -0.03815746F;
                stand_flip.neck.X = 0.006085184F;
                stand_flip.neck.Y = 0.3479513F;
                stand_flip.neck.Z = -0.01025343F;
                stand_data[50] = stand_flip;
                stand_flip.head.X = 0.02215517F;
                stand_flip.head.Y = 0.5393919F;
                stand_flip.head.Z = -0.0381794F;
                stand_flip.neck.X = 0.006085177F;
                stand_flip.neck.Y = 0.3479485F;
                stand_flip.neck.Z = -0.01025319F;
                stand_data[51] = stand_flip;
                stand_flip.head.X = 0.02215115F;
                stand_flip.head.Y = 0.5393809F;
                stand_flip.head.Z = -0.03820848F;
                stand_flip.neck.X = 0.006082613F;
                stand_flip.neck.Y = 0.3479458F;
                stand_flip.neck.Z = -0.01025128F;
                stand_data[52] = stand_flip;
                stand_flip.head.X = 0.02214572F;
                stand_flip.head.Y = 0.5393722F;
                stand_flip.head.Z = -0.03823936F;
                stand_flip.neck.X = 0.006077843F;
                stand_flip.neck.Y = 0.3479434F;
                stand_flip.neck.Z = -0.01024866F;
                stand_data[53] = stand_flip;
                stand_flip.head.X = 0.02213963F;
                stand_flip.head.Y = 0.5393681F;
                stand_flip.head.Z = -0.03826463F;
                stand_flip.neck.X = 0.006071418F;
                stand_flip.neck.Y = 0.3479434F;
                stand_flip.neck.Z = -0.01024497F;
                stand_data[54] = stand_flip;
                stand_flip.head.X = 0.02213305F;
                stand_flip.head.Y = 0.5393646F;
                stand_flip.head.Z = -0.03828752F;
                stand_flip.neck.X = 0.006063099F;
                stand_flip.neck.Y = 0.3479438F;
                stand_flip.neck.Z = -0.01024246F;
                stand_data[55] = stand_flip;
                stand_flip.head.X = 0.02212631F;
                stand_flip.head.Y = 0.539363F;
                stand_flip.head.Z = -0.03830564F;
                stand_flip.neck.X = 0.006051884F;
                stand_flip.neck.Y = 0.347945F;
                stand_flip.neck.Z = -0.01024115F;
                stand_data[56] = stand_flip;
                stand_flip.head.X = 0.02211842F;
                stand_flip.head.Y = 0.539363F;
                stand_flip.head.Z = -0.03832138F;
                stand_flip.neck.X = 0.006038151F;
                stand_flip.neck.Y = 0.3479468F;
                stand_flip.neck.Z = -0.01024067F;
                stand_data[57] = stand_flip;
                stand_flip.head.X = 0.02210155F;
                stand_flip.head.Y = 0.5393617F;
                stand_flip.head.Z = -0.03834569F;
                stand_flip.neck.X = 0.00602388F;
                stand_flip.neck.Y = 0.3479488F;
                stand_flip.neck.Z = -0.01023924F;
                stand_data[58] = stand_flip;
                stand_flip.head.X = 0.02208195F;
                stand_flip.head.Y = 0.5393601F;
                stand_flip.head.Z = -0.03836954F;
                stand_flip.neck.X = 0.006008042F;
                stand_flip.neck.Y = 0.3479497F;
                stand_flip.neck.Z = -0.0102365F;
                stand_data[59] = stand_flip;

            }
        }
    }
}