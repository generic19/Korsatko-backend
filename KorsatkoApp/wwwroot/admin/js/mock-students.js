const mockStudents = [
    {
        "name": "عياض مبارك رمضان شهاب",
        "nationalId": "30210105959413"
    },
    {
        "name": "صادق مؤنس سلامة فارس",
        "nationalId": "30311238204665"
    },
    {
        "name": "عطا يامن فاروق محسن",
        "nationalId": "30108086521165"
    },
    {
        "name": "قيس ركان شكري سماح",
        "nationalId": "30206065640260"
    },
    {
        "name": "سريج مجيد عطا حذيفة",
        "nationalId": "30207165642638"
    },
    {
        "name": "سريج مبارك راشد مبارك",
        "nationalId": "30107112801387"
    },
    {
        "name": "كامل رجب ذاكر حليم",
        "nationalId": "30110238090745"
    },
    {
        "name": "مسعود عباس ريان رضوان",
        "nationalId": "30105205336474"
    },
    {
        "name": "ريان مرسي ياسر حسام",
        "nationalId": "30205137823821"
    },
    {
        "name": "شكري عامر يامن عاصي",
        "nationalId": "30112128314669"
    },
    {
        "name": "عباس توفيق ناصر فاروق",
        "nationalId": "30110274879829"
    },
    {
        "name": "مرسي بركات رمضان مسعود",
        "nationalId": "30012094164651"
    },
    {
        "name": "مصلح مجدي عطا فرج",
        "nationalId": "30205147307447"
    },
    {
        "name": "شفيق رامز نصحي فاروق",
        "nationalId": "30109206991162"
    },
    {
        "name": "سفيان رائف ركان بسام",
        "nationalId": "30302169661446"
    },
    {
        "name": "راشد سفيان نصحي شكري",
        "nationalId": "30203189164066"
    },
    {
        "name": "أمان رضوان خالد صفوان",
        "nationalId": "30104056985263"
    },
    {
        "name": "سماح سماح عاطف فرج",
        "nationalId": "30511272231857"
    },
    {
        "name": "كاظم نزار منيب منيب",
        "nationalId": "30107091152244"
    },
    {
        "name": "عياض صفوان حليم خليل",
        "nationalId": "30506071677095"
    },
    {
        "name": "فرج حازم دريد نبيل",
        "nationalId": "30207098465859"
    },
    {
        "name": "ريان نبيل ذاكر ظافر",
        "nationalId": "30112099728402"
    },
    {
        "name": "على صادق رياض سامر",
        "nationalId": "30008096312454"
    },
    {
        "name": "سامر نصحي فرج رمضان",
        "nationalId": "30110245154794"
    },
    {
        "name": "مجيد ذاكر سلامة عياض",
        "nationalId": "30309050619205"
    },
    {
        "name": "صادق كمال حسام سلامة",
        "nationalId": "30510247374154"
    },
    {
        "name": "يامن رضا ظافر رمضان",
        "nationalId": "30213063899889"
    },
    {
        "name": "رمضان ياسين نزار سامر",
        "nationalId": "30404207837432"
    },
    {
        "name": "ركان عماد ياسر مجدي",
        "nationalId": "30011269399818"
    },
    {
        "name": "مؤنس حازم عباس فاروق",
        "nationalId": "30303230266085"
    },
    {
        "name": "مصلح مرسي رامز خليل",
        "nationalId": "30003043470305"
    },
    {
        "name": "سامر صادق ذاكر رضا",
        "nationalId": "30302263071562"
    },
    {
        "name": "معين وسيم فارس شكري",
        "nationalId": "30206052676692"
    },
    {
        "name": "حازم رضا عياض المقداد",
        "nationalId": "30304079645262"
    },
    {
        "name": "قيس عطا نبيل منيب",
        "nationalId": "30201252014092"
    },
    {
        "name": "سامي مؤنس مخلص خليل",
        "nationalId": "30104039970524"
    },
    {
        "name": "سفيان وسيم رضوان ",
        "nationalId": "30312162525336"
    },
    {
        "name": "رضا شكري سريج عباس",
        "nationalId": "30303159741698"
    },
    {
        "name": "نبيل حليم ظافر حسام",
        "nationalId": "30001143223498"
    },
    {
        "name": "ظافر تيسير نهاد معين",
        "nationalId": "30108074467434"
    },
    {
        "name": "مبارك فارس كامل منيب",
        "nationalId": "30406079357478"
    },
    {
        "name": "فهمي منيب رؤوف حسام",
        "nationalId": "30408206844319"
    },
    {
        "name": "نهاد صفوان نبيل نزار",
        "nationalId": "30409287126443"
    },
    {
        "name": "مسعود هيكل توفيق على",
        "nationalId": "30510034288744"
    },
    {
        "name": "حليم رضا منيب حفيظ",
        "nationalId": "30402264611294"
    },
    {
        "name": "بركات رؤوف فرج قيس",
        "nationalId": "30008262152585"
    },
    {
        "name": "كامل كمال راشد حسام",
        "nationalId": "30007074192688"
    },
    {
        "name": "حازم فرج عياض رمضان",
        "nationalId": "30304191540653"
    },
    {
        "name": "رؤوف يامن عادل صفوان",
        "nationalId": "30011120702917"
    },
    {
        "name": "خالد مجيد عامر حسام",
        "nationalId": "30406226354223"
    },
    {
        "name": "منيب كاظم رياض رشاد",
        "nationalId": "30404154244459"
    },
    {
        "name": "أمان فهمي غفار حليم",
        "nationalId": "30305022515923"
    },
    {
        "name": "عاصي سماح عادل رمضان",
        "nationalId": "30207212976589"
    },
    {
        "name": "مؤنس حذيفة رضا ياسر",
        "nationalId": "30110262809374"
    },
    {
        "name": "ياسين المقداد ذاكر سامر",
        "nationalId": "30310040449094"
    },
    {
        "name": "مجدي عطا رامز سفيان",
        "nationalId": "30202077850182"
    },
    {
        "name": "فرج رؤوف على رمضان",
        "nationalId": "30411268036414"
    },
    {
        "name": "ظافر نزار سامي ذاكر",
        "nationalId": "30309065079641"
    },
    {
        "name": "مؤنس ياسين نبيل مجيد",
        "nationalId": "30201147790491"
    },
    {
        "name": "مجيد رضا فريد كمال",
        "nationalId": "30505056610547"
    },
    {
        "name": "رشاد توفيق رائف منيب",
        "nationalId": "30509248579143"
    },
    {
        "name": "كمال وسيم رضا حليم",
        "nationalId": "30409130712631"
    },
    {
        "name": "عياض كمال وسيم مجيد",
        "nationalId": "30408032980256"
    },
    {
        "name": "وسيم رضوان فهمي صادق",
        "nationalId": "30101101000795"
    },
    {
        "name": "ظافر حازم حذيفة فهمي",
        "nationalId": "30108055901624"
    },
    {
        "name": "سماح مجيد صفوان معين",
        "nationalId": "30507127962238"
    },
    {
        "name": "مخلص فارس سامي دريد",
        "nationalId": "30101050462859"
    },
    {
        "name": "توفيق وسيم على منيب",
        "nationalId": "30406050945592"
    },
    {
        "name": "أحمد فكري صفوان محسن",
        "nationalId": "30004269529946"
    },
    {
        "name": "أمان ناصر سماح نبيل",
        "nationalId": "30406127471866"
    },
    {
        "name": "شكري شهاب ظافر شهاب",
        "nationalId": "30305035254025"
    },
    {
        "name": "فارس بسام رامز عامر",
        "nationalId": "30504083170966"
    },
    {
        "name": "صادق سامر ياسر المقداد",
        "nationalId": "30509060640401"
    },
    {
        "name": "فاروق عياض عادل راشد",
        "nationalId": "30210217780348"
    },
    {
        "name": "على ظافر شكري فهمي",
        "nationalId": "30306260389865"
    },
    {
        "name": "فهمي سلامة ياسين منيب",
        "nationalId": "30507220036828"
    },
    {
        "name": "خليل حذيفة فهمي حازم",
        "nationalId": "30506025436667"
    },
    {
        "name": "حفيظ سامي رضوان فارس",
        "nationalId": "30008066890507"
    },
    {
        "name": "رمضان مصلح فريد عياض",
        "nationalId": "30302202480121"
    },
    {
        "name": "بركات نبيل حازم ريان",
        "nationalId": "30305056133202"
    },
    {
        "name": "أحمد صادق صادق عادل",
        "nationalId": "30402200595865"
    },
    {
        "name": "ياسر سامي مصلح يامن",
        "nationalId": "30207232722057"
    },
    {
        "name": "حليم ياسين صادق رجب",
        "nationalId": "30209058706065"
    },
    {
        "name": "مبارك سفيان حذيفة رمضان",
        "nationalId": "30209146414705"
    },
    {
        "name": "مصلح رضوان عباس شهاب",
        "nationalId": "30211081656911"
    },
    {
        "name": "سامر ريان أحمد راشد",
        "nationalId": "30001136354668"
    },
    {
        "name": "بسام تيسير مؤنس عادل",
        "nationalId": "30305049111934"
    },
    {
        "name": "خليل فارس نزار رياض",
        "nationalId": "30408271283117"
    },
    {
        "name": "عرفات بركات على يامن",
        "nationalId": "30302251896894"
    },
    {
        "name": "فاروق فاروق يامن سريج",
        "nationalId": "30505270739729"
    },
    {
        "name": "بركات حفيظ ريان نهاد",
        "nationalId": "30507097101354"
    },
    {
        "name": "مصلح فاروق حليم عياض",
        "nationalId": "30508027437762"
    },
    {
        "name": "فاروق قيس عاطف مؤنس",
        "nationalId": "30203269544178"
    },
    {
        "name": "مخلص كمال أمان سماح",
        "nationalId": "30204097883451"
    },
    {
        "name": "ياسر ناصر فاروق منيب",
        "nationalId": "30104029342352"
    },
    {
        "name": "مجدي رامز نهاد شكري",
        "nationalId": "30010056584115"
    },
    {
        "name": "أمان ريان فاروق دريد",
        "nationalId": "30106059166021"
    },
    {
        "name": "رمضان فادي رؤوف أحمد",
        "nationalId": "30013025702292"
    },
    {
        "name": "على ذاكر شفيق شفيق",
        "nationalId": "30111232767705"
    },
    {
        "name": "رضوان يامن منيب ذاكر",
        "nationalId": "30405084598907"
    }
];