using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitBody
{
    public class SearchViewModelforDiet : INotifyPropertyChanged
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

        public SearchViewModelforDiet()
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
            "1. Jajecznica:\r\nUbij jajka w misce i lekko posól.\r\nRozgrzej patelnię z niewielką ilością oleju lub masła.\r\nWlej roztrzepane jajka na patelnię i smaż na średnim ogniu.\r\nPo kilku minutach, gdy jajka zaczną się ścięwać, delikatnie mieszaj je, aż będą odpowiednio konsystentne.\r\nPodawaj z ulubionymi dodatkami, takimi jak szczypiorek, pomidory lub ser. \r\n",
            "2. Kanapka z awokado i jajkiem:\r\nRozgnieć dojrzałe awokado w misce i dodaj sok z cytryny, sól i pieprz do smaku.\r\nPrzygotuj jajka sadzone na miękko lub twardo.\r\nRozprowadź pastę z awokado na kromce pieczywa.\r\nNa jednej kromce połóż pokrojone jajko i przykryj drugą kromką chleba.\r\nMożesz również dodać dodatkowe składniki, takie jak plastry pomidora czy boczek. \r\n",
            "3. Sałatka z tuńczykiem:\r\nW dużej misce umieść pokrojone sałatę, pokrojone pomidory, pokrojone ogórki i pokrojoną cebulę.\r\nDodaj do miski odcedzony tuńczyk wędzony lub w wodzie.\r\nWymieszaj wszystkie składniki.\r\nPrzygotuj sos, łącząc oliwę z oliwek, sok z cytryny, sól i pieprz.\r\nPolej sałatkę sosem i dokładnie wymieszaj. \r\n",
            "4. Kurczak po grecku z ziemniakami:\r\nPokrój kurczaka na kawałki i natrzyj solą, pieprzem, oregano i sokiem z cytryny.\r\nNa rozgrzanej patelni z olejem lub masłem smaż kurczaka z obu stron, aż stanie się złoty.\r\nDodaj do kurczaka pokrojone pomidory, pokrojone czerwone cebule i pokrojone ziemniaki.\r\nDopraw solą, pieprzem i oregano, a następnie przykryj patelnię i dusz składniki na małym ogniu, aż ziemniaki będą miękkie.\r\nPodawaj z kawałkami oliwek i fetą. \r\n",
            "5. Sałatka z grillowanym kurczakiem i awokado:\r\nGrilluj filety z kurczaka, aż będą dobrze upieczone.\r\nPokrój grillowany kurczak na kawałki.\r\nW dużej misce umieść mieszankę sałat, taką jak rukola lub sałata lodowa.\r\nDodaj do miski pokrojone pomidory, pokrojone awokado i pokrojoną czerwoną cebulę.\r\nPrzygotuj dressing, łącząc oliwę z oliwek, ocet balsamiczny, musztardę, sól i pieprz.\r\nPolej sałatkę sosem, dodaj kawałki grillowanego kurczaka i delikatnie wymieszaj. \r\n",
            "6. Owsianka:\r\nW rondelku zagotuj mleko lub wodę.\r\nDodaj płatki owsiane i gotuj na średnim ogniu, często mieszając.\r\nDodaj ulubione składniki, takie jak owoce, orzechy, cynamon czy miód.\r\nGotuj owsiankę, aż osiągnie pożądaną konsystencję.\r\nPodawaj ciepłą owsiankę w misce, udekorowaną dodatkowymi składnikami. \r\n",
            "7. Tosty z awokado i jajkiem:\r\nPrzygotuj tosty, smarując kromki chleba masłem lub oliwą z oliwek.\r\nNa patelni usmaż jajka sadzone na miękko lub twardo.\r\nPokrój awokado na plasterki.\r\nNa jednej kromce chleba rozsmaruj plasterki awokado, a na drugiej umieść smażone jajko.\r\nPrzypraw tosty solą, pieprzem i dodatkowymi przyprawami według upodobań. \r\n",
            "8. Kanapki z łososiem i ogórkiem:\r\nRozsmaruj na kromkach pieczywa ulubiony sos, na przykład chrzanowy lub jogurtowy.\r\nNa jednej kromce połóż plastry wędzonego łososia.\r\nPokrój ogórka na cienkie plasterki i umieść je na drugiej kromce chleba.\r\nZłożone kromki razem, tworząc kanapki.\r\nMożesz również dodać świeże zioła lub warzywa do smaku. \r\n",
            "9. Sałatka z kurczakiem i awokado:\r\nUgotuj lub upiecz filety z kurczaka, aż będą dobrze przyrumienione.\r\nPokrój kurczaka na kawałki.\r\nW dużej misce umieść mieszankę sałat, taką jak sałata rzymska lub sałata masłowa.\r\nDodaj do miski pokrojone awokado, pokrojone pomidory i pokrojoną cebulę.\r\nPrzygotuj dressing, łącząc jogurt grecki, sok z limonki, posiekany czosnek, sól i pieprz.\r\nPolej sałatkę sosem, dodaj kawałki kurczaka i delikatnie wymieszaj. \r\n",
            "10. Kurczak curry z warzywami i ryżem:\r\nPokrój filety z kurczaka na kawałki i podsmaż je na rozgrzanej patelni.\r\nDodaj do kurczaka posiekane cebule, posiekany czosnek, pokrojone papryki i inne ulubione warzywa.\r\nPrzygotuj sos curry, łącząc mleko kokosowe, pastę curry, sos sojowy, sok z limonki i inne przyprawy.\r\nDodaj sos curry do patelni z kurczakiem i warzywami, i dusz wszystko na średnim ogniu, aż warzywa staną się miękkie.\r\nPodawaj kurczaka curry z ugotowanym ryżem. \r\n",
            "11. Makaron z krewetkami i pesto:\r\nUgotuj makaron al dente, zgodnie z instrukcjami na opakowaniu.\r\nNa rozgrzanej patelni podsmaż krewetki z czosnkiem, solą i pieprzem.\r\nDodaj ugotowany makaron do patelni z krewetkami.\r\nDodaj ulubione pesto do smaku i delikatnie wymieszaj.\r\nPodawaj makaron z krewetkami i pesto, posypany startym parmezanem. \r\n",
            "12. Ryż z warzywami i tofu:\r\nUgotuj ryż wg instrukcji na opakowaniu.\r\nNa patelni podsmaż pokrojone tofu z ulubionymi przyprawami.\r\nDodaj do patelni pokrojone warzywa, takie jak marchewka, papryka i groszek.\r\nSmaż warzywa i tofu, aż staną się miękkie.\r\nDodaj ugotowany ryż do patelni, wymieszaj i dopraw do smaku.\r\nPodawaj ryż z warzywami i tofu jako zdrową i sycącą potrawę. \r\n",
            "13.Kanapki z hummusem i warzywami:\r\nRozsmaruj grubą warstwę hummusu na kromkach pieczywa.\r\nPokrój ulubione warzywa na plasterki, takie jak ogórek, pomidor, papryka czy marchewka.\r\nUłóż plasterki warzyw na jednej kromce chleba.\r\nPrzypraw warzywa solą, pieprzem i innymi ulubionymi przyprawami.\r\nZłożone kromki razem, tworząc kanapki z hummusem i warzywami. \r\n",
            "14.Kanapki z indykiem i warzywami:\r\nPokrój mięso z indyka na cienkie plastry.\r\nRozsmaruj ulubiony sos na kromkach pieczywa.\r\nNa jednej kromce ułóż plastry indyka.\r\nDodaj pokrojone warzywa, takie jak sałata, pomidor, ogórek i czerwona cebula.\r\nPrzypraw kanapki solą, pieprzem i innymi ulubionymi przyprawami.\r\nPrzykryj drugą kromką chleba, tworząc kanapki z indykiem i warzywami. \r\n",
            "15. Sałatka z tuńczykiem i jajkiem:\r\nW dużej misce umieść mieszankę sałat, taką jak mieszanka sałatowa lub sałata rzymska.\r\nDodaj do miski odcedzony tuńczyk wędzony lub w wodzie.\r\nDodaj ugotowane i pokrojone na plasterki jajka.\r\nPokrój pomidory, ogórki i cebulę i dodaj je do miski.\r\nPrzygotuj dressing, łącząc oliwę z oliwek, ocet jabłkowy, musztardę, sól i pieprz.\r\nPolej sałatkę sosem, delikatnie wymieszaj i podawaj. \r\n"
        };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
