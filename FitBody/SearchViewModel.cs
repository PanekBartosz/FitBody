using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitBody
{
    public class SearchViewModel : INotifyPropertyChanged
    {
        private string _searchText;
        private ObservableCollection<string> _filteredItems;

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    FilterItems();
                    OnPropertyChanged(nameof(SearchText));
                }
            }
        }

        public ObservableCollection<string> FilteredItems
        {
            get { return _filteredItems; }
            set
            {
                _filteredItems = value;
                OnPropertyChanged(nameof(FilteredItems));
            }
        }

        public SearchViewModel()
        {
            FilteredItems = new ObservableCollection<string>(GetAllItems());
        }

        private void FilterItems()
        {
            FilteredItems.Clear();
            string searchText = SearchText?.ToLower();

            if (!string.IsNullOrEmpty(searchText))
            {
                foreach (string item in GetAllItems())
                {
                    if (item.ToLower().Contains(searchText))
                    {
                        FilteredItems.Add(item);
                    }
                }
            }
            else
            {
                foreach (string item in GetAllItems())
                {
                    FilteredItems.Add(item);
                }
            }
        }

        private IEnumerable<string> GetAllItems()
        {
            return new List<string>
        {
            "1. Dynamiczne rozciąganie: \r\n-Wykonuj dynamiczne ruchy rozciągające, takie jak krążenie ramion, przysiady z wypchnięciem ramion do góry itp. \r\n",
            "2. Przysiady ze sztangą na karku:\r\n   - Umieść sztangę na karku, stojąc prosto z nogami rozstawionymi na szerokość barków.\r\n   - Powoli opuść ciało w dół, zginając kolana, aż uda będą równoległe do podłogi.\r\n   - Następnie powoli wróć do pozycji wyjściowej, prostując nogi.\r\n",
            "3. Wyciskanie sztangi na ławce poziomej:\r\n   - Połóż się na ławce poziomej, trzymając sztangę na wysokości klatki piersiowej, ręce wyprostowane.\r\n   - Powoli opuść sztangę do linii klatki piersiowej.\r\n   - Następnie napnij mięśnie klatki piersiowej i ramion, unosząc sztangę do góry.\r\n",
            "4. Wiosłowanie sztangą w opadzie tułowia:\r\n   - Stój z lekko ugiętymi kolanami, trzymając sztangę przed sobą na wyciągniętych rękach.\r\n   - Pochyl się do przodu, utrzymując prostą pleców.\r\n   - Pociągnij sztangę do góry, unosząc łokcie do tyłu, aż dotkniesz jej dolną częścią klatki piersiowej.\r\n",
            "5. Martwy ciąg:\r\n   - Stań prosto z nogami rozstawionymi na szerokość barków, trzymając sztangę na wyciągniętych rękach przed sobą.\r\n   - Skłon się w przód, zginając biodra i kolana, opuszczając sztangę w dół.\r\n   - Unieś sztangę do góry, napinając mięśnie pleców i nóg.\r\n",
            "6. Wyciskanie hantli na barki:\r\n   - Stań prosto, trzymając hantle na wysokości ramion, dłonie skierowane do przodu.\r\n   - Unieś hantle do góry, prostując ramiona.\r\n   - Powoli opuść hantle do pozycji wyjściowej.\r\n",
            "7. Unoszenie nóg w leżeniu na ławce poziomej:\r\n   - Połóż się na ławce poziomej, trzymając się jej ramy.\r\n   - Unieś nogi prosto do góry, napinając mięśnie brzucha.\r\n   - Powoli opuść nogi do pozycji wyjściowej.\r\n",
            "8. Przysiad bułgarski z hantlami:\r\n   - Stojąc prost, wykonaj krok w tył jedną nogą.\r\n   - Zegnij kolano przedniej nogi, opuszczając się w dół, aż tylna noga prawie dotknie podłogi.\r\n   - Wróć do pozycji wyjściowej i powtórz ćwiczenie na drugą nogę.\r\n",
            "9. Wyciskanie sztangi nad głowę:\r\n   - Stań prosto, trzymając sztangę na wysokości barków, dłonie szeroko rozstawione.\r\n   - Unieś sztangę do góry, prostując ramiona.\r\n   - Powoli opuść sztangę do pozycji wyjściowej.\r\n",
            "10. Podciąganie na drążku szerokim uchwytem:\r\n   - Złap drążek szerokim uchwytem, ręce szerzej niż szerokość barków.\r\n   - Powoli podciągnij ciało do góry, napinając mięśnie pleców i ramion.\r\n   - Opuszczaj się powoli do pozycji wyjściowej.\r\n",
            "11. Wspięcia na palcach stojąc:\r\n   - Stań prosto, nogi razem, ręce z boków.\r\n   - Unieś się na palce stóp, unosząc pięty jak najwyżej.\r\n   - Powoli opuść pięty do pozycji wyjściowej.\r\n",
            "12. Przyciąganie linki wyciągu dolnego do klatki piersiowej:\r\n   - Stań prosto, trzymając linkę wyciągu dolnego w obu rękach, dłonie skierowane do siebie.\r\n   - Pociągnij linkę do klatki piersiowej, napinając mięśnie pleców.\r\n   - Powoli wróć do pozycji wyjściowej, kontrolując ruch linki.\r\n",
            "13. Skłony boczne z hantlami:\r\n   - Stań prosto, trzymając hantle w obu rękach, dłonie wzdłuż ciała.\r\n   - Skręć tułów w bok, opuszczając jedną hantlę wzdłuż uda.\r\n   - Powróć do pozycji wyjściowej i powtórz na drugą stronę.\r\n",
            "14. Wyciskanie hantli w leżeniu na ławce skośnej:\r\n   - Połóż się na ławce skośnej, trzymając hantle na wysokości klatki piersiowej, ręce wyprostowane.\r\n   - Powoli opuść hantle do linii klatki piersiowej.\r\n   - Następnie napnij mięśnie klatki piersiowej i ramion, unosząc hantle do góry.\r\n",
            "15. Przysiady ze sztangą na barkach:\r\n   - Umieść sztangę na górnej części pleców, stojąc prosto z nogami rozstawionymi na szerokość barków.\r\n   - Powoli opuść ciało w dół, zginając kolana, aż uda będą równoległe do podłogi.\r\n   - Następnie powoli wróć do pozycji wyjściowej, prostując nogi.\r\n",
            "16. Wiosłowanie sztangą w opadzie tułowia (wiosłowanie na czworaka):\r\n   - Stój z lekko ugiętymi kolanami, trzymając sztangę przed sobą na wyciągniętych rękach.\r\n   - Pochyl się do przodu, utrzymując prostą pleców.\r\n   - Pociągnij sztangę do góry, unosząc łokcie do tyłu, aż dotkniesz jej dolną częścią klatki piersiowej.\r\n",
            "17. Unoszenie nóg w zwisie na drążku:\r\n   - Złap drążek szerokim uchwytem, ręce na szerokość barków.\r\n   - Unieś nogi prosto do góry, napinając mięśnie brzucha.\r\n   - Powoli opuść nogi do pozycji wyjściowej.\r\n",
            "18. Prostowanie ramion z linkami wyciągu górnego:\r\n   - Stań prosto, trzymając linki wyciągu górnego w obu rękach, ręce skierowane do góry.\r\n   - Wyprostuj ramiona, rozszerzając linki na zewnątrz.\r\n   - Powoli zegnij ramiona, wracając do pozycji wyjściowej.\r\n",
            "19. Plank:\r\n   - Połóż się na podłodze, opierając się na przedramionach i palcach stóp.\r\n   - Napnij mięśnie brzucha i utrzymaj prostą linię ciała przez określony czas.\r\n",
            "20. Wyciskanie sztangi na ławce skośnej:\r\n   - Połóż się na ławce skośnej, trzymając sztangę na wysokości klatki piersiowej, nogi stabilnie oparte na podłożu.\r\n   - Wypchnij sztangę do góry, prostując ramiona.\r\n   - Powoli opuść sztangę do pozycji wyjściowej.\r\n",
            "21. Rozpiętki na maszynie:\r\n   - Usiądź na maszynie rozpiętek, trzymając uchwyty na wysokości klatki piersiowej.\r\n   - Rozszerzaj ramiona na boki, aż poczujesz napięcie w mięśniach klatki piersiowej.\r\n   - Powoli wracaj do pozycji wyjściowej, kontrolując ruch.\r\n",
            "22. Podciąganie na drążku:\r\n   - Chwytaj drążek nachwytem (dłonie skierowane w stronę ciała), nieco szerzej niż szerokość barków.\r\n   - Zwisaj na wyciągniętych rękach, następnie unosząc ciało, prowadź łopatki do dołu i skieruj łokcie w dół.\r\n   - Podciągnij się maksymalnie, dotykając drążka klatką piersiową, a następnie opuść się powoli do pozycji wyjściowej.\r\n",
            "23. Podciąganie na maszynie:\r\n   - Usiądź na maszynie do podciągania, chwytając uchwyty nachwytem.\r\n   - Pociągnij uchwyty w kierunku klatki piersiowej, napinając mięśnie grzbietu.\r\n   - Powoli wróć do pozycji wyjściowej, kontrolując ruch.\r\n",
            "24. Uginanie ramion ze sztangielkami w siadzie:\r\n   - Usiądź na ławce, trzymając sztangielki wzdłuż ciała, dłonie skierowane w dół.\r\n   - Uginaj ramiona w łokciach, przyciągając sztangielki do barków.\r\n   - Powoli opuść sztangielki do pozycji wyjściowej.\r\n",
            "25. Face pulls:\r\n   - Złap wyciąg na wysokości klatki piersiowej, trzymając uchwyty nachwytem.\r\n   - Pociągnij uchwyty w kierunku twarzy, unosząc łokcie na wysokość oczu.\r\n   - Powoli wróć do pozycji wyjściowej, kontrolując ruch.\r\n",
            "26. Wspięcia na palce stojąc:\r\n   - Stań na podłożu, trzymając się czegoś dla równowagi.\r\n   - Podnoś pięty, unosząc się na palcach stóp.\r\n   - Powoli opuść pięty do pozycji wyjściowej.\r\n",
            "27. Prostowanie nóg na maszynie:\r\n   - Usiądź na maszynie do prostowania nóg, trzymając uchwyty stabilnie.\r\n   - Wyprostuj nogi, unosząc obciążenie na maszynie.\r\n   - Powoli zginaj nogi, wracając do pozycji wyjściowej.\r\n",
            "28. Wypychanie na suwnicy:\r\n   - Usiądź na suwnicy z podniesioną piętą na podłożu i barkami przylegającymi do oparcia.\r\n   - Ugnij kolana i powoli wypchnij suwnicę w górę, prostując nogi.\r\n   - Powoli opuść suwnicę do pozycji wyjściowej.\r\n",
            "29. Uginanie nóg w leżeniu na brzuchu:\r\n   - Połóż się na brzuchu na ławce, z nogami swobodnie zwisającymi.\r\n   - Zgiń nogi w stawach kolanowych, unosząc stopy ku pośladkom.\r\n   - Powoli opuść nogi do pozycji wyjściowej.\r\n",
            "30. Prostowanie nóg na maszynie w siadzie:\r\n   - Usiądź na maszynie do prostowania nóg, z nogami na podstawie i kolana ugięte pod kątem 90 stopni.\r\n   - Wypchnij platformę maszyny, prostując nogi przed sobą.\r\n   - Powoli zgiągnij nogi, wracając do pozycji wyjściowej.\r\n",
            "31. Wspięcia na palce w staniu:\r\n   - Stań na podłodze, stopy szerokości barków.\r\n   - Unieś się na palce, podnosząc pięty jak najwyżej.\r\n   - Powoli opuść się, wracając do pozycji wyjściowej.\r\n",
            "32. Wyciskanie sztangi na ławeczce (skos dodatni):\r\n   - Połóż się na ławeczce skośnej (głową wyżej).\r\n   - Trzymając sztangę na wysokości klatki piersiowej, wypchnij ją do góry, prostując ramiona.\r\n   - Powoli opuść sztangę do pozycji wyjściowej.\r\n",
            "33. Wyciskanie hantli w leżeniu:\r\n   - Leż na ławce płaskiej, trzymając hantle na wysokości klatki piersiowej.\r\n   - Wypchnij hantle do góry, prostując ramiona.\r\n   - Powoli opuść hantle do pozycji wyjściowej.\r\n",
            "34. Rozpiętki:\r\n   - Leż na ławce płaskiej, trzymając hantle w dłoniach nad klatką piersiową, dłonie skierowane na zewnątrz.\r\n   - Powoli rozsuń ręce na boki, opuszczając hantle na boki, aż poczujesz napięcie w klatce piersiowej.\r\n   - Wróć do pozycji wyjściowej, powoli unosząc hantle.\r\n",
            "35. Rozpiętki w siadzie na maszynie:\r\n   - Usiądź na maszynie do rozpiętek, trzymając uchwyty na wysokości klatki piersiowej.\r\n   - Wypchnij uchwyty do przodu, rozsuwając ramiona.\r\n   - Powoli złącz ramiona, wracając do pozycji wyjściowej.\r\n",
            "36. Przenoszenie hantla za głowę:\r\n    - Stań prosto, trzymając hantel w jednej ręce.\r\n    - Unieś hantel do góry, tak aby znajdowała się za głową.\r\n    - Powoli opuść hantel do pozycji wyjściowej.\r\n",
            "37. Podciąganie na drążku nachwytem:\r\n    - Chwytaj drążek szeroko, dłonie zwrócone w stronę ciała.\r\n    - Zawisnij na wyciągniętych ramionach.\r\n    - Napnij mięśnie pleców i ramion, unosząc się w górę, aż podbródek minie wysokość drążka.\r\n    - Powoli opuść się, wracając do pozycji wyjściowej.\r\n",
            "38. Wiosłowanie sztangą:\r\n    - Nachyl się do przodu, trzymając sztangę w wąskim chwycie, dłonie zwrócone ku sobie.\r\n    - Wyprostuj plecy i napnij mięśnie pleców.\r\n    - Podciągnij sztangę do góry, prowadząc łokcie wzdłuż tułowia.\r\n    - Powoli opuść sztangę, wracając do pozycji wyjściowej.\r\n",
            "39. Ściąganie drążka wyciągu górnego stojąc:\r\n    - Stój przed maszyną z drążkiem górnym, chwytając go szeroko, dłonie zwrócone do ciebie.\r\n    - Zegnij lekko nogi, utrzymując stabilną pozycję.\r\n    - Pociągnij drążek w dół, skierowując łokcie w dół i do tyłu.\r\n    - Powoli wróć do pozycji wyjściowej, kontrolując ruch.\r\n",
            "40. Wznosy barków (szrugsy):\r\n    - Stań prosto, trzymając sztangę na wyciągniętych ramionach przed sobą, dłonie zwrócone do ciała.\r\n    - Unieś ramiona, unosząc sztangę jak najwyżej wzdłuż boków.\r\n    - Delikatnie opuść sztangę, wracając do pozycji wyjściowej.\r\n",
            "41. Ściąganie rączki wyciągu górnego w siadzie:\r\n    - Usiądź na ławce z napiętymi mięśniami pleców.\r\n    - Chwytaj rączkę wyciągu górnego, dłonie zwrócone do ciebie.\r\n    - Pociągnij rączkę w dół, skierowując łokcie w dół i do tyłu.\r\n    - Powoli wróć do pozycji wyjściowej, kontrolując ruch.\r\n",
            "42. Wyciskanie sztangi z przed głowy:\r\n    - Stań prosto, trzymając sztangę na wysokości klatki piersiowej, chwyt szeroki.\r\n    - Wypchnij sztangę do góry, prostując ramiona.\r\n    - Powoli opuść sztangę, wracając do pozycji wyjściowej.\r\n",
            "43. Podciąganie sztangi wzdłuż tułowia:\r\n    - Stań prosto, trzymając sztangę w wąskim chwycie, dłonie zwrócone ku sobie.\r\n    - Unieś sztangę do góry, prowadząc ją wzdłuż tułowia.\r\n    - Powoli opuść sztangę, wracając do pozycji wyjściowej.\r\n",
            "44. Unoszenie ramion z hantlem bokiem w leżeniu:\r\n    - Połóż się na ławce, trzymając hantle wzdłuż boków.\r\n    - Unieś hantle na boki, prowadząc je na wysokość ramion.\r\n    - Powoli opuść hantle, wracając do pozycji wyjściowej.\r\n",
            "45. Unoszenie ramion w przód z hantlami:\r\n    - Stań prosto, trzymając hantle z wyciągniętymi ramionami przed sobą.\r\n    - Unieś hantle prosto do przodu, na wysokość ramion.\r\n    - Powoli opuść hantle, wracając do pozycji wyjściowej.\r\n",
            "46. Unoszenie ramion bokiem w opadzie tułowia z linkami wyciągu dolnego:\r\n    - Stań z lekko pochyloną górną częścią ciała, trzymając linki wyciągu dolnego w dłoniach.\r\n    - Unieś linki na boki, prowadząc je wzdłuż ciała na wysokość ramion.\r\n    - Powoli opuść linki, wracając do pozycji wyjściowej.\r\n",
            "47. Uginanie ramion na modlitewniku:\r\n    - Usiądź na modlitewniku, trzymając ręce na uchwytach z dłońmi skierowanymi do siebie.\r\n    - Zegnij ramiona, unosząc dłonie ku sobie.\r\n    - Powoli wróć do pozycji wyjściowej, kontrolując ruch.\r\n",
            "48. Wyciskanie francuskie sztangą łamaną:\r\n    - Leż na ławce, trzymając sztangę łamaną w wąskim chwycie nad głową.\r\n    - Zegnij ramiona, opuszczając sztangę za głowę.\r\n    - Powoli wyprostuj ramiona, unosząc sztangę do pozycji wyjściowej.\r\n",
            "49. Uginanie ramion \"młotkowe\":\r\n    - Stań prosto, trzymając hantle wzdłuż boków z chwytem młotkowym.\r\n    - Zegnij ramiona, unosząc hantle do boków ramion.\r\n    - Powoli opuść hantle, wracając do pozycji wyjściowej.\r\n",
            "50. Pompki na poręczach (dipy):\r\n    - Podpieraj się na poręczach, rękoma z tyłu i nogami z przodu.\r\n    - Zegnij ramiona, opuszczając ciało w dół.\r\n    - Wyprostuj ramiona, unosząc ciało do góry.\r\n    - Powoli wróć do pozycji wyjściowej.\r\n",
            "51. Uginanie ramion ze sztangą:\r\n    - Stań prosto, trzymając sztangę w chwycie nachwytem, dłonie zwrócone do ciała.\r\n    - Zegnij ramiona, unosząc sztangę ku górze.\r\n    - Powoli opuść sztangę, wracając do pozycji wyjściowej.\r\n",
            "52. Skłony w leżeniu płasko:\r\n    - Połóż się na ławce, trzymając hantle na wysokości klatki piersiowej.\r\n    - Zegnij tułów w dół, opuszczając hantle ku podłodze.\r\n    - Powoli wyprostuj się, wracając do pozycji wyjściowej.\r\n",
            "53. Unoszenie nóg w leżeniu na ławeczce:\r\n    - Połóż się na ławeczce, trzymając się jej górnej części.\r\n    - Unieś nogi, prowadząc je do góry, jak najwyżej.\r\n    - Powoli opuść nogi, wracając do pozycji wyjściowej.\r\n",
            "54. Skłony tułowia z linką wyciągu klęcząc:\r\n    - Klęknij na podłodze, trzymając linkę wyciągu dolnego w rękach.\r\n    - Zegnij tułów w dół, prowadząc linkę w stronę podłogi.\r\n    - Powoli wyprostuj się, wracając do pozycji wyjściowej.\r\n"
        };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
