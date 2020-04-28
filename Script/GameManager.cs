using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{

    public GameObject PlayButton;
    
    public Texture[] CardTexture;
    public Sprite[] CardSprite;
    public Sprite BackCard;

    public Text player1Hand;
    public Text player2Hand;
    public Text gameResult;
    public GameObject gameResultPanel;

    public int MatchCardValueAndWho1;
    public int MatchCardValueAndWho11;
    public int MatchCardValueAndWho2;
    public int MatchCardValueAndWho22;

    public int TotalPlayer1Score;
    public int TotalPlayer2Score;

    public Text MatchValueWho1;
    public Text MatchValueWho2;

    public Image player1card1Image;
    public Image player1card2Image;
    public Image player2card1Image;
    public Image player2card2Image;

    public Image publiccard1Image;
    public Image publiccard2Image;
    public Image publiccard3Image;
    public Image publiccard4Image;
    public Image publiccard5Image;

    public Image[] allcardImages;

    public List<Card> card = new List<Card>();
    public List<CardsValue> cardValue = new List<CardsValue>();
 

    public List<Card> player1card = new List<Card>(); 
    public List<Card> player2card = new List<Card>(); 
    public List<Card> publiccard = new List<Card>(); 

    public List<Card> player1publiccard = new List<Card>();
    public List<Card> player2publiccard = new List<Card>(); 
    

    private List<Card> playerClubs = new List<Card>(); 
    private List<Card> playerDiamonds = new List<Card>();
    private List<Card> playerHearts = new List<Card>();
    private List<Card> playerSpades = new List<Card>();
    
    public Text ButtonText;
    
    public int takenValue1;
    public int takenValue2;

    public int cardCount ;
    public bool RandomCard = true;
    public Text RandomCardText;
    public bool SortCard = false;
    public bool ReverseCard =false;

    public int LowerBreakerCard1;
    public int LowerBreakerCard2;
    public bool HighCardBreaker1 = false;
    public bool HighCardBreaker2 = false;

    // Start is called before the first frame update
    
    void Start(){

        string RandomString = PlayerPrefs.GetString("Random","Random");    
        
        if(RandomString == "Random"){
            RandomCard = true;
            RandomCardText.text = "Random";
        }else{
            RandomCard = false;
            RandomCardText.text = "Sorted";
        }

        ReStart();
    }
    
    void ReStart()
    {
        PlayButton.SetActive(false);
        
        StartCoroutine( ClearAll());

        int num = 0;
        int cardNumber = 2; 
        for( int i = 0; i < 13 ; i++){
                card.Add(new Card("Clubs", cardNumber , cardNumber++, CardSprite[num++]));
        }
        cardNumber = 2; 
        for( int i = 0; i < 13 ; i++){
                card.Add(new Card("Diamonds", cardNumber , cardNumber++, CardSprite[num++]));
        }
        cardNumber = 2; 
        for( int i = 0; i < 13 ; i++){
                card.Add(new Card("Hearts", cardNumber , cardNumber++, CardSprite[num++]));
        }
        cardNumber = 2; 
        for( int i = 0; i < 13 ; i++){
                card.Add(new Card("Spades", cardNumber , cardNumber++, CardSprite[num++]));
        }


        if(SortCard){
            card.Sort();
        }
        
        if(ReverseCard){
            card.Reverse();
        }
        
        //
        
        
        foreach (Card cardResult in card)
        {
         //  Debug.Log( cardResult.name + " : "+ cardResult.value1 + " : "+ cardResult.sprite.ToString());
            cardCount++;
        }

        StartCoroutine(SetCard(card));
    }

    public void RandomIt(){
        if(RandomCard){
            RandomCard = false;
            RandomCardText.text = "Sorted";
            PlayerPrefs.SetString("Random","Sorted");
        }else{
            RandomCard =true;
            RandomCardText.text = "Random";
            PlayerPrefs.SetString("Random","Random");
        }
    }

    public void SortIt(){
        if(SortCard){
            SortCard = false;
        } else{
            SortCard = true;
        }

    }

    public void ReverseIt(){
        if(ReverseCard){
            ReverseCard = false;
        }else{
            ReverseCard = true;
        }


    }




    public IEnumerator SetCard(List<Card> card){
        
        yield return new WaitForSeconds(2.0f);
        //Debug.Log("SetCard Function");
        float timer = 0.1f;
        SetPlayerCard(card , player1card, player1card1Image);
        yield return new WaitForSeconds(timer);
        SetPlayerCard(card , player1card, player1card2Image);
        yield return new WaitForSeconds(timer);
        SetPlayerCard(card , player2card, player2card1Image);        
        yield return new WaitForSeconds(timer);
        SetPlayerCard(card , player2card, player2card2Image);
        yield return new WaitForSeconds(timer);
        SetPlayerCard(card , publiccard, publiccard1Image);
        yield return new WaitForSeconds(timer);
        SetPlayerCard(card , publiccard, publiccard2Image);
        yield return new WaitForSeconds(timer);
        SetPlayerCard(card , publiccard, publiccard3Image);
        yield return new WaitForSeconds(timer);
        SetPlayerCard(card , publiccard, publiccard4Image);
        yield return new WaitForSeconds(timer);
        SetPlayerCard(card , publiccard, publiccard5Image);
        yield return new WaitForSeconds(timer);

        CheckCards();        
    }


    void SetPlayerCard(List<Card> card, List<Card> playercard, Image cardImage){
       
       int i = 0;
       if(RandomCard){
           i = Random.Range(0,cardCount);
           
       }
       
       
                       

        //Debug.Log(cardCount);
       // Debug.Log(card[i].name + card[i].value1);  
            playercard.Add(new Card(card[i].name ,card[i].value1  , card[i].value2, card[i].sprite ));
            cardImage.GetComponent<Image>().sprite = card[i].sprite;


       // string cardSpriteName = CardSprite[i].ToString();
            card.Remove(card[i]); // remove or take the card from the deck 
            cardCount = cardCount - 1;
        //Debug.Log("cardCount:"+cardCount);

        /*
        if(card[i].value1 == takenValue1 || card[i].value1 == takenValue2){
                Debug.Log("Re-SetPlayerCard" + card[i].value1);
                SetPlayerCard(card,playercard,cardImage);                
        }else{
        }
         */   
            /*
         if(takenValue1 == 0) {
                takenValue1= card[i].value1;
            }else if(takenValue2 == 0){
                takenValue2 = card[i].value1 ;
            }else{
              //  Debug.Log("no value");
            } 
            */
            
        
    }

    void CheckCards(){


          //  Debug.Log("takenValue1=" + takenValue1);
          //  Debug.Log("takenValue2=" + takenValue2);

        for (int i = 0; i < 2; i++)
        {
            player1publiccard.Add(new Card(player1card[i].name, player1card[i].value1, player1card[i].value2, player1card[i].sprite));
            player2publiccard.Add(new Card(player2card[i].name, player2card[i].value1, player2card[i].value2, player2card[i].sprite));
        }


        for (int i = 0; i < 5; i++)
        {
            player1publiccard.Add(new Card(publiccard[i].name, publiccard[i].value1, publiccard[i].value2, publiccard[i].sprite));
            player2publiccard.Add(new Card(publiccard[i].name, publiccard[i].value1, publiccard[i].value2, publiccard[i].sprite));
        }


            
        

       // Debug.Log("cardCount:"+cardCount);
        
       // player1publiccard.Sort();

         foreach (Card playercards in player1publiccard)
        {
       //  Debug.Log("Player1Cards:" + playercards.value1 + playercards.name );
        }

        foreach (Card playercards in player2publiccard)
        {
       //  Debug.Log("Player2Cards:" + playercards.value1 + playercards.name );
        }

        /*
        foreach(Card p in removeLastExcessCard(player1publiccard)){
            Debug.Log("AfterRemovePlayer1Cards:" + p.value1 + p.name );
        }
        */


        //CompareCards(player1publiccard, player2publiccard);
        StartCoroutine(IcompareCards(player1publiccard, player2publiccard));
    }



    IEnumerator IcompareCards(List<Card> player1publiccard, List<Card> player2publiccard){
        Debug.Log("IcompareCards");
        EvaluateCard(player1publiccard, player1card, "player1");
        yield return new WaitForSeconds(1.5f);
        EvaluateCard(player2publiccard, player2card, "player2");
        yield return new WaitForSeconds(1.5f);
        Tally();
        yield return new WaitForSeconds(1.0f);
        PlayButton.SetActive(true);
    }

    void Tally(){
        Debug.Log("Tally");

        if(TotalPlayer1Score == 0 && TotalPlayer2Score == 0){
            gameResultPanel.SetActive(true);
            Debug.Log("Draw");
            gameResult.text = "Draw";

        }else


        if(TotalPlayer1Score == TotalPlayer2Score){
                
                if(MatchCardValueAndWho1 > MatchCardValueAndWho2){
                    Debug.Log("Player1 Wins");
                    gameResultPanel.SetActive(true);
                    gameResult.text = "Player 1 Wins";

                } else if(MatchCardValueAndWho1 < MatchCardValueAndWho2){
                    Debug.Log("Player2 Wins");
                    gameResultPanel.SetActive(true);
                gameResult.text = "Player 2 Wins";
                 } else if(MatchCardValueAndWho1 == MatchCardValueAndWho2 && MatchCardValueAndWho1 == 0 ){
                   
                     
                            if(MatchCardValueAndWho11 > MatchCardValueAndWho22){
                                Debug.Log("Player1 Wins");
                                gameResultPanel.SetActive(true);
                                gameResult.text = "Player 1 Wins";
                            }else if(MatchCardValueAndWho11 < MatchCardValueAndWho22){
                                Debug.Log("Player2 Wins");
                                gameResultPanel.SetActive(true);
                                gameResult.text = "Player 2 Wins";
                            }else{

                                    if(HighCard2(player1card,"player1") > HighCard2(player2card,"player2")){
                                                Debug.Log("Player1 Wins");
                                                gameResultPanel.SetActive(true);
                                                gameResult.text = "Player 1 Wins";
                                    } else if(HighCard2(player1card,"player1") < HighCard2(player2card,"player2")){
                                                Debug.Log("Player2 Wins");
                                                gameResultPanel.SetActive(true);
                                                gameResult.text = "Player 2 Wins";
                                    }else{
                                               

                                                if(HighCardBreaker1 == HighCardBreaker2){
                                                        Debug.Log("HighCardBreakers");
                                                    if(LowerBreakerCard1 > LowerBreakerCard2){
                                                            Debug.Log("Player1 Wins");
                                                            gameResultPanel.SetActive(true);
                                                            gameResult.text = "Player 1 Wins";
                                                            setTextLabel("HighCard:player1");

                                                        }else  if(LowerBreakerCard1 < LowerBreakerCard2){
                                                            Debug.Log("Player2 Wins");
                                                            gameResultPanel.SetActive(true);
                                                            gameResult.text = "Player 2 Wins";
                                                            setTextLabel("HighCard:player2");

                                                        } else{
                                                            gameResultPanel.SetActive(true);
                                                            Debug.Log("Draw");
                                                            gameResult.text = "Draw";

                                                    }  



                                                }else{
                                                        gameResultPanel.SetActive(true);
                                                        Debug.Log("Draw");
                                                        gameResult.text = "Draw";
     


                                                }

                                                
                                                /*
                                               if(LowCard(player1card,"player1") > LowCard(player2card,"player2")){
                                                   Debug.Log("Player1 Wins");
                                                gameResultPanel.SetActive(true);
                                                gameResult.text = "Player 1 Wins"; 

                                               }else if(LowCard(player1card,"player1") < LowCard(player2card,"player2")){
                                                  Debug.Log("Player2 Wins");
                                                gameResultPanel.SetActive(true);
                                                gameResult.text = "Player 2 Wins";      

                                               }else{
                                                gameResultPanel.SetActive(true);
                                                Debug.Log("Draw");
                                                gameResult.text = "Draw";

                                               }
                                               */
                                                
                                    }


                            }
                   
                 }                                  
                  else {
                                    /*
                                     if(HighCard2(player1card,"player1") > HighCard2(player2card,"player2")){
                                                Debug.Log("Player1 Wins");
                                                gameResultPanel.SetActive(true);
                                                gameResult.text = "Player 1 Wins";
                                    } else if(HighCard2(player1card,"player1") < HighCard2(player2card,"player2")){
                                                Debug.Log("Player2 Wins");
                                                gameResultPanel.SetActive(true);
                                                gameResult.text = "Player 2 Wins";
                                    }else{
                                            */   


                                            if(processCommunityCardwithPlayerCard(player1publiccard, player1card , "player1") > processCommunityCardwithPlayerCard(player2publiccard, player2card , "player2")){
                                                Debug.Log("Player1 Wins");
                                                gameResultPanel.SetActive(true);
                                                gameResult.text = "Player 1 Wins";

                                            }else

                                            if(processCommunityCardwithPlayerCard(player1publiccard, player1card , "player1") < processCommunityCardwithPlayerCard(player2publiccard, player2card , "player2")){
                                                Debug.Log("Player2 Wins");
                                                gameResultPanel.SetActive(true);
                                                gameResult.text = "Player 2 Wins";

                                            }else{

                                                gameResultPanel.SetActive(true);
                                                Debug.Log("Draw");
                                                gameResult.text = "Draw";

                                            }

                                                /*

                                               if(LowCard(player1card,"player1") > LowCard(player2card,"player2")){
                                                   Debug.Log("Player1 Wins");
                                                gameResultPanel.SetActive(true);
                                                gameResult.text = "Player 1 Wins"; 

                                               }else if(LowCard(player1card,"player1") < LowCard(player2card,"player2")){
                                                  Debug.Log("Player2 Wins");
                                                gameResultPanel.SetActive(true);
                                                gameResult.text = "Player 2 Wins";      

                                               }else{
                                                gameResultPanel.SetActive(true);
                                                Debug.Log("Draw");
                                                gameResult.text = "Draw";

                                               }
                                               */
                                               
                                   /*             
                                    }
                                    */
                        }
        }else if(TotalPlayer1Score > TotalPlayer2Score){
            Debug.Log("Player1 Wins");
            gameResultPanel.SetActive(true);
          gameResult.text = "Player 1 Wins";

        }else if(TotalPlayer1Score < TotalPlayer2Score){
            Debug.Log("Player2 Wins");
            gameResultPanel.SetActive(true);
          gameResult.text = "Player 2 Wins";

        }else{
                gameResultPanel.SetActive(true);
                Debug.Log("Draw");
                gameResult.text = "Draw";
        }
            Debug.Log("TotalPlayer1Score " + TotalPlayer1Score );
            Debug.Log("MatchCardValueAndWho1 " + MatchCardValueAndWho1);
            Debug.Log("TotalPlayer2Score " + TotalPlayer2Score );
            Debug.Log("MatchCardValueAndWho2 " + MatchCardValueAndWho2);
            Debug.Log("HighCardBreaker1 = " + HighCardBreaker1);
            Debug.Log("HighCardBreaker2 = " + HighCardBreaker2);
    }


    void CompareCards(List<Card> player1publiccard, List<Card> player2publiccard){
    


        if(EvaluateCard(player1publiccard, player1card, "player1") == EvaluateCard(player2publiccard, player2card, "player2")){            
          

            if(MatchCardValueAndWho1 > MatchCardValueAndWho2){
                    Debug.Log("Player1 Wins");
                    gameResultPanel.SetActive(true);
                gameResult.text = "Player 1 Wins";

            } else if(MatchCardValueAndWho1 < MatchCardValueAndWho2){
                    Debug.Log("Player2 Wins");
                    gameResultPanel.SetActive(true);
                gameResult.text = "Player 2 Wins";
                
            } else {
                Debug.Log("Draw");
                gameResultPanel.SetActive(true);
                gameResult.text = "Draw";
 
          }


          //Debug.Log(EvaluateCard(player1publiccard, playercard1, "player1") + " : " + EvaluateCard(player2publiccard, playercard2, "player2"));
          return;
        }else if(EvaluateCard(player1publiccard, player1card, "player1") > EvaluateCard(player2publiccard, player2card, "player2")){            
          Debug.Log("Player1 Wins");
          gameResultPanel.SetActive(true);
          gameResult.text = "Player 1 Wins";
          
         // Debug.Log(EvaluateCard(player1publiccard, playercard1, "player1") + " : " + EvaluateCard(player2publiccard, playercard2, "player2"));
          return;
        } else if(EvaluateCard(player1publiccard, player1card, "player1") < EvaluateCard(player2publiccard, player2card, "player2")){            
          Debug.Log("Player2 Wins");
          gameResultPanel.SetActive(true);
          gameResult.text = "Player 2 Wins";
         // Debug.Log(EvaluateCard(player1publiccard, playercard1, "player1") + " : " + EvaluateCard(player2publiccard, playercard2, "player2"));
          return;
        } else {
            Debug.Log("What???");  
          //  Debug.Log(EvaluateCard(player1publiccard, playercard1, "player1") + " : " + EvaluateCard(player2publiccard, playercard2, "player2"));
            return;
        }

        //Debug.Log(EvaluateCard(player1publiccard, player1card, "player1"));
        //Debug.Log(EvaluateCard(player2publiccard, player2card, "player2"));
        
        
    }

     public int EvaluateCard(List<Card> pc, List<Card> defaultCards, string playerWho){

                if(RoyalFlush(pc, defaultCards).Contains("RoyalFlush")){
                        Debug.Log("RoyalFlush :"+ playerWho);
                        setTextLabel("RoyalFlush :"+ playerWho); 
                        if(playerWho == "player1"){TotalPlayer1Score= 1000;}else{TotalPlayer2Score = 1000;}   
                        return 1000;                                             
                }else if(RoyalFlush(pc, defaultCards).Contains("RoyalFlusX")){
                        Debug.Log("RoyalFlusX :"+ playerWho);
                        setTextLabel(" :"+ playerWho);
                        if(playerWho == "player1"){TotalPlayer1Score= 0 ;}else{TotalPlayer2Score = 0 ;}    
                        return 0;                                             
                }else if(StraightFlush(pc, defaultCards, playerWho).Contains("StraightFlush")){
                        Debug.Log("StraightFlush :"+ playerWho);
                        setTextLabel("StraightFlush :"+ playerWho);
                        if(playerWho == "player1"){TotalPlayer1Score= 900 + HighCard(defaultCards,playerWho) ;}else{TotalPlayer2Score = 900 + HighCard(defaultCards,playerWho);}
                        return 900 + HighCard(defaultCards,playerWho);                         
                }else if(StraightFlush(pc, defaultCards, playerWho).Contains("StraightFlusX")){
                        Debug.Log("StraightFlusX :"+ playerWho);
                        setTextLabel(" :"+ playerWho);
                        if(playerWho == "player1"){TotalPlayer1Score= 0 ;}else{TotalPlayer2Score = 0;}
                        return 0;
                }else if(FourOfAKind(pc, defaultCards, playerWho).Contains("FourOfAKind")){
                        Debug.Log("FourOfAKind :"+ playerWho);
                        setTextLabel("FourOfAKind :"+ playerWho);
                        if(playerWho == "player1"){TotalPlayer1Score= 800+ HighCard(defaultCards,playerWho);}else{TotalPlayer2Score = 800+ HighCard(defaultCards,playerWho);}
                        return 800 + HighCard(defaultCards,playerWho);
                }else if(FourOfAKind(pc, defaultCards, playerWho).Contains("FourOfAKinX")){
                        Debug.Log("FourOfAKinX :"+ playerWho);
                        setTextLabel(":"+ playerWho);
                        if(playerWho == "player1"){TotalPlayer1Score= 1;}else{TotalPlayer2Score = 1;}
                        return 1;

                }else if(Flush(pc, defaultCards, playerWho).Contains("Flush")){
                        Debug.Log("Flush :"+ playerWho);
                        setTextLabel("Flush :"+ playerWho);
                        if(playerWho == "player1"){TotalPlayer1Score= 700+ HighCard(defaultCards,playerWho);}else{TotalPlayer2Score = 700+ HighCard(defaultCards,playerWho);}
                        return 700 + HighCard(defaultCards,playerWho);
                                                                        
                
                }else if(Flush(pc, defaultCards, playerWho).Contains("FlusX")){
                        Debug.Log("FlusX :"+ playerWho);
                        setTextLabel(" :"+ playerWho);
                        if(playerWho == "player1"){TotalPlayer1Score= 0;}else{TotalPlayer2Score = 0;}
                        return 0;

                }else if(FullHouse(pc, defaultCards, playerWho).Contains("FullHouse")){
                        Debug.Log("FullHouse:"+ playerWho);
                        setTextLabel("FullHouse:"+ playerWho);
                        if(playerWho == "player1"){TotalPlayer1Score= 600+ HighCard(defaultCards,playerWho);}else{TotalPlayer2Score = 600+ HighCard(defaultCards,playerWho);}
                        return 600 + HighCard(defaultCards,playerWho);  
                
                }else if(FullHouse(pc, defaultCards, playerWho).Contains("FullHousX")){
                        Debug.Log("FullHousX:"+ playerWho);
                        setTextLabel(":"+ playerWho);
                        if(playerWho == "player1"){TotalPlayer1Score= 0;}else{TotalPlayer2Score = 0;}
                        return 0;

                }else if(Straight(pc, defaultCards, playerWho).Contains("Straight")){
                        Debug.Log("Straight:"+ playerWho);
                        setTextLabel("Straight:"+ playerWho);
                        if(playerWho == "player1"){TotalPlayer1Score= 500+ HighCard(defaultCards,playerWho);}else{TotalPlayer2Score = 500+ HighCard(defaultCards,playerWho);}
                        return 500 + HighCard(defaultCards,playerWho); 

                }else if(Straight(pc, defaultCards, playerWho).Contains("StraighX")){
                        Debug.Log("StraighX:"+ playerWho);
                        setTextLabel(":"+ playerWho);
                        if(playerWho == "player1"){TotalPlayer1Score= 0;}else{TotalPlayer2Score = 0;}
                        return 0;

                }else if(ThreeOfAKind(pc, defaultCards, playerWho).Contains("ThreeOfAKind")){
                        Debug.Log("ThreeOfAKind:"+ playerWho);
                        setTextLabel("ThreeOfAKind:"+ playerWho);
                        if(playerWho == "player1"){TotalPlayer1Score= 400+ HighCard(defaultCards,playerWho);}else{TotalPlayer2Score = 400+ HighCard(defaultCards,playerWho);}
                        return 400 + HighCard(defaultCards,playerWho);                                                                        

                }else if(TwoPairs(pc, defaultCards, playerWho).Contains("TwoPairs")){
                        Debug.Log("TwoPairs:"+ playerWho);
                        setTextLabel("TwoPairs:"+ playerWho);
                        if(playerWho == "player1"){TotalPlayer1Score= 300+ HighCard(defaultCards,playerWho);}else{TotalPlayer2Score = 300+ HighCard(defaultCards,playerWho);}
                        return 300 + HighCard(defaultCards,playerWho);                                                
                
                }else if(OnePair(pc, defaultCards, playerWho).Contains("OnePair")){
                        Debug.Log("OnePair:"+ playerWho);
                        setTextLabel("OnePair:"+ playerWho);
                        if(playerWho == "player1"){TotalPlayer1Score= 200+ HighCard(defaultCards,playerWho);}else{TotalPlayer2Score = 200+ HighCard(defaultCards,playerWho);}
                        return 200 + HighCard(defaultCards,playerWho);                                                
                }
                 else {
                    


                    
                    if(processCommunityCard(publiccard, playerWho) == "NoCombination"){
                        
                        /*
                        if(playerWho == "player1"){TotalPlayer1Score= 0;}else{TotalPlayer2Score = 0;}
                        MatchCardValueAndWho1 = 0;
                        MatchCardValueAndWho2 = 0;
                        return 0;
                         */   

                            return FinalizeTheCard(pc,defaultCards, playerWho);


                    } else{

                        return processCommunityCardwithPlayerCard(pc, defaultCards, playerWho);
                        
                        

                    }



                    
                    
                    
                    
                    
                }           
    }


    private int FindWhereThePlayerCardIs(List<Card> pairCards, List<Card> defaultCards, string playerWho, string Textlabel){

            


            foreach (Card c in pairCards)
            {
                Debug.Log("PairCards" + c.value1);
            }
            


            int card1 = 10;
            int card2 = 10;
                for(int i = 0; i < 6; i++){
                    if(pairCards[i].value1 == defaultCards[0].value1){ card1 = i; }
                    if(pairCards[i].value1 == defaultCards[1].value1){ card2 = i; }
                }

                Debug.Log(playerWho + " card1 " + card1);
                Debug.Log(playerWho + " card2 " + card2);

                if(card1 > 4 && card2 > 4){
                    
                    return 0; 
                }else if(card1 < 5 && card2 < 5){
                    

                    setTextLabel(Textlabel + ":"+ playerWho);
                    Debug.Log( playerWho + " both card is in first 5"); 

                    if(playerWho == "player1"){
                            TotalPlayer1Score = HighCard2(defaultCards,playerWho);
                            LowerBreakerCard1 = LowCard(defaultCards,playerWho);
                            HighCardBreaker1 = true;
                        }else{
                            TotalPlayer2Score = HighCard2(defaultCards,playerWho);
                            LowerBreakerCard2 = LowCard(defaultCards,playerWho);
                            HighCardBreaker2 = true;
                        }

                    
                    return HighCard2(defaultCards,playerWho);
                
                
                }else if(card1 < 5 || card2 < 5){

                        
                         Debug.Log( playerWho + " one of card is in first 5");        
                        if(playerWho == "player1"){
                            TotalPlayer1Score = HighCard2(defaultCards, playerWho); 
                            HighCardBreaker1 = true;                           
                        }else{
                            TotalPlayer2Score = HighCard2(defaultCards, playerWho); 
                            HighCardBreaker2 = true;                           
                        }
                        
                        return HighCard2(defaultCards,playerWho);

                }else{
                    return 0;
                }



    }


private int processCommunityCardwithPlayerCard(List<Card> pc, List<Card> defaultCards, string playerWho){

        Debug.Log("processCommunityCardwithPlayerCard for " + playerWho);

    foreach(Card p in pc){
        Debug.Log("commcards: "+ p.value1);
    }

        // find combination
        // if no combination = draw
        // if theres combination  = show what kind of combination then  highcard ranking

        bool four = false;
        int which = 0; 
        // check for 4 pairs
        for (int i = 0 ; i < 3 ; i++){
            if(pc[i].value1 == pc[i + 1].value1 && pc[i + 1].value1 == pc[i + 2].value1 && pc[i + 2].value1 == pc[i + 3].value1){                
                // make 4 pair and one kicker 
                four = true;
                which = i;
                Debug.Log("FourPairsFound");               
            }else{
                Debug.Log("FourPairs Not Found");
            }
        }

        List<Card> FourPairSetup = new List<Card>();

        if(four){            
                FourPairSetup.Add(pc[which]);
                FourPairSetup.Add(pc[which + 1]);
                FourPairSetup.Add(pc[which + 2]);
                FourPairSetup.Add(pc[which + 3]);                

                if(which == 0){
                    FourPairSetup.Add(pc[4]);
                    FourPairSetup.Add(pc[5]);
                    FourPairSetup.Add(pc[6]);                    
                }
                if(which == 1){
                    FourPairSetup.Add(pc[0]);
                    FourPairSetup.Add(pc[5]);
                    FourPairSetup.Add(pc[6]);
                }
                if(which == 2){
                    FourPairSetup.Add(pc[0]);
                    FourPairSetup.Add(pc[1]);
                    FourPairSetup.Add(pc[6]);
                }
                if(which == 3){
                    FourPairSetup.Add(pc[0]);
                    FourPairSetup.Add(pc[1]);
                    FourPairSetup.Add(pc[2]);
                }

                setTextLabel("FourOfAKind:"+ playerWho);
                return FindWhereThePlayerCardIs(FourPairSetup, defaultCards, playerWho, "FourOfAKind");
        
        
        
        }


        



        bool three = false;
        which = 0; 
        // check for 3 pairs
        for (int i = 0 ; i < 4 ; i++){
            if(pc[i].value1 == pc[i + 1].value1 && pc[i + 1].value1 == pc[i + 2].value1){
                three = true;
                which = i; 
                Debug.Log("ThreePairsFound");
            }else{
                Debug.Log("ThreePairs Not Found");    

            }
        }

        List<Card> ThreePairSetup = new List<Card>();
        if(three){

                ThreePairSetup.Add(pc[which]);
                ThreePairSetup.Add(pc[which + 1]);
                ThreePairSetup.Add(pc[which + 2]);


                if(which == 0){
                    ThreePairSetup.Add(pc[3]);
                    ThreePairSetup.Add(pc[4]);
                    ThreePairSetup.Add(pc[5]);
                    ThreePairSetup.Add(pc[6]);
                }
                if(which == 1){
                    ThreePairSetup.Add(pc[0]);
                    ThreePairSetup.Add(pc[4]);
                    ThreePairSetup.Add(pc[5]);
                    ThreePairSetup.Add(pc[6]);
                }
                if(which == 2){
                    ThreePairSetup.Add(pc[0]);
                    ThreePairSetup.Add(pc[1]);
                    ThreePairSetup.Add(pc[5]);
                    ThreePairSetup.Add(pc[6]);
                }
                if(which == 3){
                    ThreePairSetup.Add(pc[0]);
                    ThreePairSetup.Add(pc[1]);
                    ThreePairSetup.Add(pc[2]);
                    ThreePairSetup.Add(pc[6]);
                }
                if(which == 4){
                    ThreePairSetup.Add(pc[0]);
                    ThreePairSetup.Add(pc[1]);
                    ThreePairSetup.Add(pc[2]);
                    ThreePairSetup.Add(pc[3]);
                }

                //find where player card is
                setTextLabel("ThreeOfAKind:"+ playerWho);
                return FindWhereThePlayerCardIs(ThreePairSetup, defaultCards, playerWho, "ThreeOfAKind");

        }


        pc.Sort();
        pc.Reverse();

        int pairs = 0;
        which = 0;
        bool two = false;
        bool one = false;
        int pairOne = 10;
        int pairTwo = 10;
        List<Card> pairsFound = new List<Card>();
        // check for pairs
        for (int i = 0 ; i < 6 ; i++){
            if(pc[i].value1 == pc[i + 1].value1 ){
                pairs++;
                which = i;
                pairsFound.Add(pc[i]);
                pairsFound.Add(pc[i + 1]);  
                if(pairs == 1){pairOne = i ;}
                if(pairs == 2){pairTwo = i ;}    
            }else{
               
            }
        }
        
    



        Debug.Log("Pairs = " + pairs + " : " + playerWho );
        Debug.Log("pairOne " + pairOne );
        Debug.Log("pairTwo " + pairTwo);
        
        if(pairs == 2){
            Debug.Log("TwoPairs");
            setTextLabel("TwoPairs:"+ playerWho);
            two = true;
            //return "TwoPairs";
        }else if (pairs == 1){
            Debug.Log("OnePair");
            //return "OnePair";
            setTextLabel("OnePair:"+ playerWho);
            one = true;
        }else{
            Debug.Log("NoCombination "+ playerWho);
            //return "NoCombination";
            return 0;
        }
        
        if(two){

               if(pairOne == 0 && pairTwo == 2){
                    pairsFound.Add(pc[4]);
                    pairsFound.Add(pc[5]);
                    pairsFound.Add(pc[6]);
               }

               if(pairOne == 0 && pairTwo == 3){
                    pairsFound.Add(pc[2]);
                    pairsFound.Add(pc[5]);
                    pairsFound.Add(pc[6]);
               } 

               if(pairOne == 0 && pairTwo == 4){
                    pairsFound.Add(pc[2]);
                    pairsFound.Add(pc[3]);
                    pairsFound.Add(pc[6]);
               } 

                if(pairOne == 0 && pairTwo == 5){
                    pairsFound.Add(pc[2]);
                    pairsFound.Add(pc[3]);
                    pairsFound.Add(pc[4]);
               }


               if(pairOne == 1 && pairTwo == 3){
                    pairsFound.Add(pc[0]);
                    pairsFound.Add(pc[5]);
                    pairsFound.Add(pc[6]);
               } 
                
                if(pairOne == 1 && pairTwo == 4){
                    pairsFound.Add(pc[0]);
                    pairsFound.Add(pc[3]);
                    pairsFound.Add(pc[6]);
               } 

                if(pairOne == 1 && pairTwo == 5){
                    pairsFound.Add(pc[0]);
                    pairsFound.Add(pc[3]);
                    pairsFound.Add(pc[4]);
               } 

                 if(pairOne == 2 && pairTwo == 4){
                    pairsFound.Add(pc[0]);
                    pairsFound.Add(pc[1]);
                    pairsFound.Add(pc[6]);
               }    

                if(pairOne == 2 && pairTwo == 5){
                    pairsFound.Add(pc[0]);
                    pairsFound.Add(pc[1]);
                    pairsFound.Add(pc[4]);
               } 

                if(pairOne == 3 && pairTwo == 5){
                    pairsFound.Add(pc[0]);
                    pairsFound.Add(pc[1]);
                    pairsFound.Add(pc[2]);
               }

                foreach(Card pF in pairsFound){
                    Debug.Log("pF "+ pF.value1);
                }

                return FindWhereThePlayerCardIs(pairsFound, defaultCards, playerWho, "TwoPairs");

        }

        if(one){

                if(which == 0){
                        pairsFound.Add(pc[2]);
                        pairsFound.Add(pc[3]);
                        pairsFound.Add(pc[4]);
                        pairsFound.Add(pc[5]);
                        pairsFound.Add(pc[6]);
                }
                if(which == 1){
                        pairsFound.Add(pc[0]);
                        pairsFound.Add(pc[3]);
                        pairsFound.Add(pc[4]);
                        pairsFound.Add(pc[5]);
                        pairsFound.Add(pc[6]);
                }
                if(which == 2){
                        pairsFound.Add(pc[0]);
                        pairsFound.Add(pc[1]);
                        pairsFound.Add(pc[4]);
                        pairsFound.Add(pc[5]);
                        pairsFound.Add(pc[6]);
                }
                if(which == 3){
                        pairsFound.Add(pc[0]);
                        pairsFound.Add(pc[1]);
                        pairsFound.Add(pc[2]);
                        pairsFound.Add(pc[5]);
                        pairsFound.Add(pc[6]);
                }
                if(which == 4){
                        pairsFound.Add(pc[0]);
                        pairsFound.Add(pc[1]);
                        pairsFound.Add(pc[2]);
                        pairsFound.Add(pc[3]);
                        pairsFound.Add(pc[6]);
                }
                if(which == 5){
                        pairsFound.Add(pc[0]);
                        pairsFound.Add(pc[1]);
                        pairsFound.Add(pc[2]);
                        pairsFound.Add(pc[3]);
                        pairsFound.Add(pc[4]);
                }

                return FindWhereThePlayerCardIs(pairsFound, defaultCards, playerWho, "OnePair");



        }

            return 0;

}
    
    private string processCommunityCard(List<Card> pc, string playerWho){

        Debug.Log("processCommunityCardOnly for "+ playerWho);

    

        pc.Sort();
        //pc.Reverse();
    
        foreach(Card p in pc){
       // Debug.Log("commcards: "+ p.value1);
        }

        // find combination
        // if no combination = draw
        // if theres combination  = show what kind of combination then  highcard ranking

        bool four = false;
        // check for 4 pairs
        for (int i = 0 ; i < 2 ; i++){
            if(pc[i].value1 == pc[i + 1].value1 && pc[i + 1].value1 == pc[i + 2].value1 && pc[i + 2].value1 == pc[i + 3].value1){
                Debug.Log("FourOfAKind");
                return "FourOfAKind";
            }

        }

        bool three = false;
        // check for 3 pairs
        for (int i = 0 ; i < 3 ; i++){
            if(pc[i].value1 == pc[i + 1].value1 && pc[i + 1].value1 == pc[i + 2].value1){
                Debug.Log("ThreeOfAKind");
                return "ThreeOfAKind";                 
            }

        }

        pc.Sort();

        int pairs = 0;
        // check for pairs
        for (int i = 0 ; i < 4 ; i++){
            if(pc[i].value1 == pc[i + 1].value1 ){
                pairs++;
               // Debug.Log(" paired "+ pc[i].value1 + "=" + pc[i + 1].value1 );
            }else{
               // Debug.Log(" no pair "+ pc[i].value1 + "x" + pc[i + 1].value1 );
            }
        }


        Debug.Log("Pairs=" + pairs + " : " + playerWho );
        if(pairs == 2){
            Debug.Log("TwoPairs");
            return "TwoPairs";
        }else if (pairs == 1){
            Debug.Log("OnePair");
            return "OnePair";
        }else{
            Debug.Log("NoCombination "+ playerWho);
            return "NoCombination";
        }
        

    }

    private int FinalizeTheCard(List<Card> pc, List<Card>  defaultCards,string playerWho){


        pc.Sort();
        pc.Reverse();

        foreach(Card p in pc){
            Debug.Log("FinalizeCards "+ playerWho + " : "+p.value1 );
        }

        int card1 = 0;
        int card2 = 0;

        for(int i=1 ; i < 7; i++){
            if(defaultCards[0].value1 == pc[i].value1){card1 = i;}
            if(defaultCards[1].value1 == pc[i].value1){card2 = i;}    
        }    

        Debug.Log("Card1 " + card1);
        Debug.Log("Card2 " + card2);

        if(card1 > 4 && card2 > 4){
            Debug.Log( playerWho + " card is not included in the first 5 cards");
            return 0;
        }else if( card1 < 5 && card2 < 5){
            Debug.Log( playerWho + " , both of card is in first 5");

                        if(playerWho == "player1"){
                            TotalPlayer1Score = HighCard2(defaultCards,playerWho) ;
                            LowerBreakerCard1 = LowCard(defaultCards,playerWho) ;
                            HighCardBreaker1 = true;
                        }else{
                            TotalPlayer2Score = HighCard2(defaultCards,playerWho) ;
                            LowerBreakerCard2 = LowCard(defaultCards,playerWho) ;
                            HighCardBreaker2 = true;
                        }
                        
                           
            return HighCard2(defaultCards,playerWho) ;

        }else if( card1 < 5 || card2 < 5){
            
            Debug.Log( playerWho + " , one of card is in first 5");
            
                        if(playerWho == "player1"){
                            TotalPlayer1Score = HighCard2(defaultCards, playerWho);
                            HighCardBreaker1 = true;
                        }else{
                            TotalPlayer2Score = HighCard2(defaultCards, playerWho);
                            HighCardBreaker2 = true;
                        }
            
            return HighCard2(defaultCards,playerWho);
        
        } else {

            Debug.Log("Nothing Ewan");    
            return 0;

        }




    }




    public void setTextLabel(string str){

        string[] t = str.ToString().Split(":"[0]);
        
        if(str.Contains("player1")){            
            player1Hand.text = t[0];
        }else{
            
            player2Hand.text = t[0];
        }        

    }


    private string RoyalFlush(List<Card> pc, List<Card> defaultCards){
      

        int isClubs = 0;
        int isDiamonds = 0;
        int isHearts = 0;
        int isSpades = 0;
        bool isAce = false;
        
        foreach(Card p in pc){
           // Debug.Log(p.value1);
            if(p.name == "Clubs"){
                isClubs++;
                playerClubs.Add(p);
            } else if(p.name == "Diamonds"){
                isDiamonds++;
                playerDiamonds.Add(p);
            }else if(p.name == "Hearts"){
                isHearts++;
                playerHearts.Add(p);
            }else if(p.name == "Spades"){
                isSpades++;
                playerSpades.Add(p);
            }else{

            }

            if(p.value1 == 14){
               // Debug.Log("Theres an ace");
                isAce = true;
            } 

        }

            if(!isAce){
                    return "Nope";
            }
            
        foreach (Card playercards in playerClubs)
        {
         //Debug.Log("Clubs:" + playercards.value1 + playercards.name );
        }

        foreach (Card playercards in playerDiamonds)
        {
        // Debug.Log("Diamonds:" + playercards.value1 + playercards.name );
        }    

        foreach (Card playercards in playerHearts)
        {
        // Debug.Log("Hearts:" + playercards.value1 + playercards.name );
        }
        foreach (Card playercards in playerSpades)
        {
         //Debug.Log("Spades:" + playercards.value1 + playercards.name );
        }

           // Debug.Log(pc[0].value1 + " : " + pc[1].value1);

            if(isClubs > 4){                   

                    return isRoyal(playerClubs,defaultCards);
            }else             

            if(isDiamonds > 4){                   
                    return isRoyal(playerDiamonds,defaultCards);
            }else

            if(isHearts > 4){                   
                   return isRoyal(playerHearts, defaultCards);
            }else 

            if(isSpades > 4){                   
                   return isRoyal(playerSpades,defaultCards);
            }else
            
            { 
                //Debug.Log("Suite less than 5");
            return "Nope"; } 

    }

    private string isRoyal(List<Card> pc, List<Card> defaultCards){

            bool ace = false;
            bool king = false;
            bool queen = false;
            bool jack = false;
            bool ten = false;

            foreach(Card p in pc){
                    if(p.value1 == 14){ ace =true; 
                    //Debug.Log("Theres an Ace"); 
                    }
            }
            foreach(Card p in pc){                    
                    if(p.value1 == 13){ king=true; 
                    //Debug.Log("Theres a King"); 
                    }
            }
            foreach(Card p in pc){
                    if(p.value1 == 12){ queen=true; 
                    //Debug.Log("Theres a Queen"); 
                    }
            }
            foreach(Card p in pc){
                    if(p.value1 == 11){ jack=true; 
                    //Debug.Log("Theres a Jack"); 
                    }
            }
            foreach(Card p in pc){
                    if(p.value1 == 10){ ten=true;  
                    //Debug.Log("Theres a 10"); 
                    }
            }

            bool userCard = false;
            if((defaultCards[0].value1 ==14) || (defaultCards[1].value1== 14)){
                    userCard = true;
            }else 
            if((defaultCards[0].value1 ==13) || (defaultCards[1].value1== 13)){
                    userCard = true;
            }else 
            if((defaultCards[0].value1 ==12) || (defaultCards[1].value1== 12)){
                    userCard = true;
            }else 
            if((defaultCards[0].value1 ==11) || (defaultCards[1].value1== 11)){
                    userCard = true;
            }else
            if((defaultCards[0].value1 ==10) || (defaultCards[1].value1== 10)){
                    userCard = true;
            }else  {
                    userCard = false;
            }



            if(ace && king && queen && jack && ten && userCard ){
                return "RoyalFlush";
            }else if(ace && king && queen && jack && ten && !userCard ){
                return "RoyalFlusX";
            }else{
                return "Nope";
            }    
    }



    private bool IsPlayerCardIncluded(List<Card> pc, List<Card> defaultCards, string playerWho){
        
        //Debug.Log("IsPlayerCardIncluded");

        int included = 0;
        bool card1 = false;
        bool card2 = false;
            foreach(Card p in pc){
                    if(p.value1 == defaultCards[0].value1){ card1 = true; }
                    if(p.value1 == defaultCards[1].value1){ card2 = true; }
            }

       // Debug.Log("IsPlayerCardIncluded result: " + card1 + card2);

         if(card1 && card2){
                if(defaultCards[0].value1 > defaultCards[1].value1){
                    MatchCardValueAndWho(defaultCards[0].value1, playerWho);
                }else{
                    MatchCardValueAndWho(defaultCards[1].value1, playerWho);
                }             
        }else if(card1){
                MatchCardValueAndWho(defaultCards[0].value1, playerWho);
        }else if(card2){
                MatchCardValueAndWho(defaultCards[1].value1, playerWho);
        }else{
            
        }



        if(card1 || card2){ 
            //Debug.Log("Included"); 
         return true; } else { return false; }

    }


    private string StraightFlush(List<Card> pc, List<Card> defaultCards, string playerWho){

        

        int isClubs = 0;
        int isDiamonds = 0;
        int isHearts = 0;
        int isSpades = 0;    
        bool isAce = false;

        List<Card> ClubCards = new List<Card>();
        List<Card> DiamondCards = new List<Card>();
        List<Card> HeartsCards = new List<Card>();
        List<Card> SpadesCards = new List<Card>();

        foreach(Card p in pc){
           // Debug.Log(p.value1);
            if(p.name == "Clubs"){
                isClubs++;
                ClubCards.Add(p);
            } else if(p.name == "Diamonds"){
                isDiamonds++;
                DiamondCards.Add(p);
            }else if(p.name == "Hearts"){
                isHearts++;
                HeartsCards.Add(p);
            }else if(p.name == "Spades"){
                isSpades++;
                SpadesCards.Add(p);
            }else{

            }

            if(p.value1 == 14){
               // Debug.Log("Theres an ace");
                isAce = true;
            } 

        }
          
        if(isClubs > 4){
               
                return checkIfStraight(removeLastExcessCard(ClubCards), defaultCards,playerWho, isAce);
            }else 
            if(isDiamonds > 4 ){
               // Debug.Log("Suite > 4");
                return checkIfStraight(removeLastExcessCard(DiamondCards),defaultCards,playerWho, isAce);
            } else
            if(isHearts > 4 ){
               // Debug.Log("Suite > 4");
                return checkIfStraight(removeLastExcessCard(HeartsCards), defaultCards,playerWho, isAce);
            }else
            if(isSpades > 4){
               // Debug.Log("Suite > 4");
                return checkIfStraight(removeLastExcessCard(SpadesCards), defaultCards,playerWho, isAce);
            }else{
                return "Nope";
            }           
            return "Nope";
        
    }


    private List<Card> removeLastExcessCard(List<Card> c ){
        //remove excess lowest card if any
        int howManyCards = 0;
        foreach(Card p in c){
                howManyCards++;
        }

        c.Sort();

            if(howManyCards == 7){
                c.Remove(c[0]);
                c.Remove(c[0]);
            }else if(howManyCards == 6){
                c.Remove(c[0]);
            }else{

            }


        return c;                    
    }


    

    private string checkIfStraight(List<Card> straightCard, List<Card> defaultCards, string playerWho, bool isAce){

          //  Debug.Log("checkIfStraight");

        straightCard.Sort();

         if((straightCard[0].value1 + 1 == straightCard[1].value1) && (straightCard[1].value1 + 1 == straightCard[2].value1) 
        && (straightCard[2].value1 + 1 == straightCard[3].value1) && (straightCard[3].value1 + 1 == straightCard[4].value1)) {

                if(IsPlayerCardIncluded(straightCard,defaultCards,playerWho)){                    
                    return "StraightFlush" ;
                } else { 
                    return "StraightFlusX" ;
                    }
        }else 
            if(isAce){

                    if(( 2 == straightCard[0].value1) && 
                        (straightCard[0].value1 + 1 == straightCard[1].value1) && 
                        (straightCard[1].value1 + 1 == straightCard[2].value1) && 
                        (straightCard[2].value1 + 1 == straightCard[3].value1)){

                        if(IsPlayerCardIncluded(straightCard,defaultCards,playerWho)){                    
                            return "StraightFlush" ;
                        } else { 
                            return "StraightFlusX" ;
                        }
                    } else{
                            return "Nope";
                    }
        
            }
        
        else {
                return "Nope" ;
        }

    }

    private string FourOfAKind(List<Card> pc, List<Card> defaultCards, string playerWho){

        //Debug.Log("Checking if FourOfAKind");
        List<Card> straightCard = new List<Card>();
        List<Card> straightCard1 = new List<Card>();
        List<Card> straightCard2 = new List<Card>();
        List<Card> straightCard3 = new List<Card>(); 

        pc.Sort();
        
            for(int i = 0; i < 4; i++){
                straightCard.Add(pc[i]);
            }

            for(int i = 1; i < 5; i++){
                straightCard1.Add(pc[i]);
            }
        
            for(int i = 2; i < 6; i++){
                straightCard2.Add(pc[i]);
            }
            for(int i = 3; i < 7; i++){
                straightCard3.Add(pc[i]);
            }

            

            if(FourPair(straightCard3, defaultCards)){
                if(IsPlayerCardIncluded(straightCard3,defaultCards,playerWho)){
                       return "FourOfAKind"; 
                }else{
                    return "FourOfAKinX";
                }                
            } else
            if(FourPair(straightCard2, defaultCards)){
                if(IsPlayerCardIncluded(straightCard2,defaultCards,playerWho)){
                       return "FourOfAKind"; 
                }else{
                    return "FourOfAKinX";
                }
            } else
            if(FourPair(straightCard1, defaultCards)){
                if(IsPlayerCardIncluded(straightCard1,defaultCards,playerWho)){
                       return "FourOfAKind"; 
                }else{
                    return "FourOfAKinX";
                }
            } else
            if(FourPair(straightCard, defaultCards)){
                if(IsPlayerCardIncluded(straightCard,defaultCards,playerWho)){
                       return "FourOfAKind"; 
                }else{
                    return "FourOfAKinX";
                }
            } else {
                return "Nope";
            }


    }

    private bool FourPair(List<Card> pc, List<Card> defaultCards){

            //Debug.Log("FourPair?");
            if(pc[0].value1 == pc[1].value1 && pc[1].value1 == pc[2].value1 && pc[2].value1 == pc[3].value1){
                return true;
            } else{
                return false;
            }

    }

    private string Flush(List<Card> pc, List<Card> defaultCards, string playerWho){

        

        int isClubs = 0;
        int isDiamonds = 0;
        int isHearts = 0;
        int isSpades = 0;    
        
        List<Card> ClubCards = new List<Card>();
        List<Card> DiamondCards = new List<Card>();
        List<Card> HeartsCards = new List<Card>();
        List<Card> SpadesCards = new List<Card>();

        foreach(Card p in pc){
           // Debug.Log(p.value1);
            if(p.name == "Clubs"){
                isClubs++;
                ClubCards.Add(p);
            } else if(p.name == "Diamonds"){
                isDiamonds++;
                DiamondCards.Add(p);
            }else if(p.name == "Hearts"){
                isHearts++;
                HeartsCards.Add(p);
            }else if(p.name == "Spades"){
                isSpades++;
                SpadesCards.Add(p);
            }else{

            }
        }
          
        if(isClubs > 4){
               
                if(IsPlayerCardIncluded(removeLastExcessCard(ClubCards), defaultCards,playerWho)){
                        return "Flush";
                }else{
                        return "FlusX";
                }
            }else 
            if(isDiamonds > 4 ){
               // Debug.Log("Suite > 4");
                if(IsPlayerCardIncluded(removeLastExcessCard(DiamondCards), defaultCards,playerWho)){
                        return "Flush";
                }else{
                        return "FlusX";
                }
            } else
            if(isHearts > 4 ){
               // Debug.Log("Suite > 4");
                if(IsPlayerCardIncluded(removeLastExcessCard(HeartsCards), defaultCards,playerWho)){
                        return "Flush";
                }else{
                        return "FlusX";
                }
            }else
            if(isSpades > 4){
               // Debug.Log("Suite > 4");
                if(IsPlayerCardIncluded(removeLastExcessCard(SpadesCards), defaultCards,playerWho)){
                        return "Flush";
                }else{
                        return "FlusX";
                }
            }else{
                return "Nope";
            }           
            return "Nope";
        
    }




    private string FullHouse(List<Card> pc, List<Card> defaultCards, string playerWho){
    
        //return "Nope";
        //Debug.Log("Checking if FullHouse");
    
        List<Card> straightCard = new List<Card>();
        List<Card> straightCard1 = new List<Card>();
        List<Card> straightCard2 = new List<Card>();
        List<Card> straightCard3 = new List<Card>(); 
        List<Card> straightCard4 = new List<Card>();

        pc.Sort();


            
            for(int i = 4; i < 7; i++){
                straightCard4.Add(pc[i]);
            }
            for(int i = 3; i < 6; i++){
                straightCard3.Add(pc[i]);
            }
            for(int i = 2; i < 5; i++){
                straightCard2.Add(pc[i]);
            }
            for(int i = 1; i < 4; i++){
                straightCard1.Add(pc[i]);
            }
            for(int i = 0; i < 3; i++){
                straightCard.Add(pc[i]);
            }
                    
            
            if(ThreePairFullHouse(straightCard4) > 0){
                if(FindLast2PairFullHouse(pc, ThreePairFullHouse(straightCard4)) > 0){
                    Debug.Log("FindLast2PairsFH" + FindLast2PairFullHouse(pc, ThreePairFullHouse(straightCard4)) );
                        if(isMyCardinFullHouse(ThreePairFullHouse(straightCard4),FindLast2PairFullHouse(pc, ThreePairFullHouse(straightCard4)),  defaultCards,playerWho)){
                            return "FullHouse"; 
                            }else{
                            return "FullHousX";
                        }
                }else{ return "Nope";}
            }else
            if(ThreePairFullHouse(straightCard3) > 0){
                if(FindLast2PairFullHouse(pc, ThreePairFullHouse(straightCard3)) > 0){
                    Debug.Log("FindLast2PairsFH" + FindLast2PairFullHouse(pc, ThreePairFullHouse(straightCard3)) );
                        if(isMyCardinFullHouse(ThreePairFullHouse(straightCard3),FindLast2PairFullHouse(pc, ThreePairFullHouse(straightCard3)),  defaultCards,playerWho)){
                            return "FullHouse"; 
                            }else{
                            return "FullHousX";
                        }
                }else{ return "Nope";}
            }else
            if(ThreePairFullHouse(straightCard2) > 0){
                if(FindLast2PairFullHouse(pc, ThreePairFullHouse(straightCard2)) > 0){
                    Debug.Log("FindLast2PairsFH" + FindLast2PairFullHouse(pc, ThreePairFullHouse(straightCard2)) );
                        if(isMyCardinFullHouse(ThreePairFullHouse(straightCard2),FindLast2PairFullHouse(pc, ThreePairFullHouse(straightCard2)),  defaultCards,playerWho)){
                            return "FullHouse"; 
                            }else{
                            return "FullHousX";
                        }
                }else{ return "Nope";}
            }else
            if(ThreePairFullHouse(straightCard1) > 0){
                if(FindLast2PairFullHouse(pc, ThreePairFullHouse(straightCard1)) > 0){
                    Debug.Log("FindLast2PairsFH" + FindLast2PairFullHouse(pc, ThreePairFullHouse(straightCard1)) );
                        if(isMyCardinFullHouse(ThreePairFullHouse(straightCard1),FindLast2PairFullHouse(pc, ThreePairFullHouse(straightCard1)),  defaultCards,playerWho)){
                            return "FullHouse"; 
                            }else{
                            return "FullHousX";
                        }
                }else{ return "Nope";}
            }else
            if(ThreePairFullHouse(straightCard) > 0){
                if(FindLast2PairFullHouse(pc, ThreePairFullHouse(straightCard)) > 0){
                    Debug.Log("FindLast2PairsFH" + FindLast2PairFullHouse(pc, ThreePairFullHouse(straightCard)) );
                        if(isMyCardinFullHouse(ThreePairFullHouse(straightCard),FindLast2PairFullHouse(pc, ThreePairFullHouse(straightCard)),  defaultCards,playerWho)){
                            return "FullHouse"; 
                            }else{
                            return "FullHousX";
                        }
                }else{ return "Nope";}
            }else{

                return "Nope";
            }
            



                  
            
    }

    private bool isMyCardinFullHouse(int BuoCardsValue,int BuoCardsValue2,  List<Card> defaultCards, string playerWho){
        
       if(defaultCards[0].value1 == BuoCardsValue){
                MatchCardValueAndWho(BuoCardsValue, playerWho);
            return true;
        }else if(defaultCards[1].value1 == BuoCardsValue){
                MatchCardValueAndWho(BuoCardsValue,playerWho);
            return true;
        }else if(defaultCards[0].value1 == BuoCardsValue2){
                MatchCardValueAndWho(BuoCardsValue2,playerWho);
            return true;
        }else if(defaultCards[1].value1 == BuoCardsValue2){
                MatchCardValueAndWho(BuoCardsValue2,playerWho);
            return true;        
        }else{
            return false;
        }
    }

    private int ThreePairFullHouse(List<Card> pc){

        //Debug.Log("ThreePair?");
            if(pc[0].value1 == pc[1].value1 && pc[1].value1 == pc[2].value1){                
               //Debug.Log("ThreePairFullHouse" + pc[0].value1);
               return pc[0].value1;
            } else{
                return 0;
            }


    } 

    private int FindLast2PairFullHouse(List<Card> pc, int  treepairValue){

        pc.Sort();
        pc.Reverse();
        int countCards = 0;
        foreach(Card p in pc){
            countCards++;
            //Debug.Log("StraightLastCards:"+countCards + " : " + p.value1 + p.name );
        }
        
        int isPair = 0;
    
        //Debug.Log("Countcards=" + countCards);

            for (int i = 0; i < 6; i++){
                   if(pc[i].value1 == pc[i + 1].value1){
                      // Debug.Log("Match Found:" + pc[i].value1 + " & " + pc[i + 1].value1);  
                      if(treepairValue != pc[i].value1){
                            isPair = pc[i].value1;
                            Debug.Log("isPair=" +isPair);                      
                      }                                            
                   } else{
                       //Debug.Log("No match");
                       
                   }
            }
        
        return isPair;


    }

    

    
    private List<Card> MinusCards(List<Card> toRemove, List<Card> removeFrom){
        Debug.Log("processing MinusCard");
        
        int toRemoveInt = 0;        
        foreach(Card p in toRemove){
                toRemoveInt++;
        }    
        Debug.Log("toRemoveInt:" + toRemoveInt);
        
        int removeFromInt=0;
        foreach(Card p in removeFrom){
            removeFromInt++;
        }
        Debug.Log("removeFromInt:" + removeFromInt);

            for(int i = 0; i < toRemoveInt; i++){
                for(int e = 0; e < removeFromInt ; e++){
                    if(toRemove[i].value1 == removeFrom[e].value1){
                        removeFrom.Remove(removeFrom[e]);
                        removeFromInt--;
                        Debug.Log("removeFromInt = "+ removeFromInt);
                    }
                }                
            }

            foreach(Card p in removeFrom){
                Debug.Log("removeFromCards : " + p.value1);                
            }

            return removeFrom;

    }






   
    private bool FindLast2Pair(List<Card> pc){

        int countCards = 0;
        foreach(Card p in pc){
            countCards++;
            //Debug.Log("StraightLastCards:"+countCards + " : " + p.value1 + p.name );
        }
        
        bool isPair = false;
        countCards--;

            for (int i = 0; i < countCards ; i++){
                   if(pc[i].value1 == pc[i + 1].value1){
                      // Debug.Log("Match Found:" + pc[i].value1 + " & " + pc[i + 1].value1);                       
                       isPair = true;                      
                   } else{
                       Debug.Log("No match");
                       
                   }
            }
        if(isPair){
            return true;
        }else{
            return false;
        }

    }


    private string Straight(List<Card> pc, List<Card> defaultCards, string playerWho){
        
        pc.Sort();
        pc.Reverse();

        List<Card> removeDuplicates = pc.Distinct().ToList();

        int rDnum = 0;

        foreach(Card p in pc){
       //Debug.Log("rd "+ playerWho+ " = " + p.value1);
        rDnum++;
        }
        //Debug.Log("rDnum" + rDnum);
    
        rDnum--;
        for(int i = 0; i < rDnum; i++){
        if(pc[i].value1 == pc[i + 1].value1){
            removeDuplicates.Remove(pc[i]);            
            rDnum = rDnum - 1;
           // Debug.Log("dups" + playerWho + " = " + pc[i].value1 );
        }else{
           // Debug.Log("not dups" + pc[i].value1 + " & " + pc[i +1].value1);

        }
        }

        int rdTotal = 0;
        foreach(Card rd in removeDuplicates){
       // Debug.Log("RD" + playerWho + " = " + rd.value1);
        rdTotal++;
        }
        int set = 0;
        if(rdTotal == 5){ set=1; }
        if(rdTotal == 6){ set=2; }
        if(rdTotal == 7){ set=3; }

        removeDuplicates.Sort();

        //Debug.Log("set " + playerWho + " : " +set);

        int HowManyStraight = 0;
        int whichSet = -1;
    
        for(int i = 0; i < set; i++){

            if((removeDuplicates[i].value1 + 1 == removeDuplicates[i + 1].value1) && (removeDuplicates[i + 1].value1 + 1 == removeDuplicates[i + 2].value1) 
        && (removeDuplicates[i + 2].value1 + 1 == removeDuplicates[i + 3].value1) && (removeDuplicates[i + 3].value1 + 1 == removeDuplicates[i + 4].value1)){ 

                   // Debug.Log("Straight Found ");
                    HowManyStraight++;
                    whichSet = i;
                }else{
                   // Debug.Log("Straight not Found");
                }

        }

        //Debug.Log("HowManyStraight = " + playerWho + " : " + whichSet);

        List<Card> straightCard = new List<Card>();

        if(whichSet == 2){
        straightCard.Add(removeDuplicates[6]);
        straightCard.Add(removeDuplicates[5]);
        straightCard.Add(removeDuplicates[4]);
        straightCard.Add(removeDuplicates[3]);
        straightCard.Add(removeDuplicates[2]);        
        }else if(whichSet == 1){
        
        straightCard.Add(removeDuplicates[5]);
        straightCard.Add(removeDuplicates[4]);
        straightCard.Add(removeDuplicates[3]);
        straightCard.Add(removeDuplicates[2]);  
        straightCard.Add(removeDuplicates[1]);  
        }else if(whichSet == 0){

        straightCard.Add(removeDuplicates[4]);
        straightCard.Add(removeDuplicates[3]);
        straightCard.Add(removeDuplicates[2]);  
        straightCard.Add(removeDuplicates[1]);
        straightCard.Add(removeDuplicates[0]);  
        }else{

        return "Nope"; 

        }

        foreach (Card s in straightCard){
        //Debug.Log("straightCard "+ playerWho + " : " + s.value1);
        }



        if(IsPlayerCardIncluded(straightCard,defaultCards,playerWho)){                    
                    return "Straight" ;
                } else { 
                    return "StraighX" ;
                    }
    

        return "Nope";

    
    


}

private bool checkIfStraight2(List<Card> straightCard, List<Card> defaultCards, string playerWho){

          //  Debug.Log("checkIfStraight");

    foreach(Card s in straightCard){
        Debug.Log("sC = " + s.value1);
    }
        
        Debug.Log(straightCard[0].value1);
        Debug.Log(straightCard[1].value1);
        Debug.Log(straightCard[2].value1);
        Debug.Log(straightCard[3].value1);
        Debug.Log(straightCard[4].value1);

         if((straightCard[0].value1 + 1 == straightCard[1].value1) && (straightCard[1].value1 + 1 == straightCard[2].value1) 
        && (straightCard[2].value1 + 1 == straightCard[3].value1) && (straightCard[3].value1 + 1 == straightCard[4].value1)) {

                return true;
        } else{
                return false ;
        }

}

    
private string ThreeOfAKind(List<Card> pc, List<Card> defaultCards, string playerWho){
    
    //Debug.Log("Checking if ThreeOfAKind");
    
    List<Card> straightCard = new List<Card>();
    List<Card> straightCard1 = new List<Card>();
    List<Card> straightCard2 = new List<Card>();
    List<Card> straightCard3 = new List<Card>(); 
    List<Card> straightCard4 = new List<Card>();

        pc.Sort();

            for(int i = 4; i < 7; i++){
                straightCard4.Add(pc[i]);
            }
            for(int i = 3; i < 6; i++){
                straightCard3.Add(pc[i]);
            }
            for(int i = 2; i < 5; i++){
                straightCard2.Add(pc[i]);
            }
            for(int i = 1; i < 4; i++){
                straightCard1.Add(pc[i]);
            }
            for(int i = 0; i < 3; i++){
                straightCard.Add(pc[i]);
            }

            if(ThreePairNoCheck(straightCard4, defaultCards, playerWho)){
                if(IsPlayerCardIncluded(straightCard4, defaultCards,playerWho)){
                        return "ThreeOfAKind";
                }else{  return "ThreeOfAKinX"; }
                
            } else
            if(ThreePairNoCheck(straightCard3, defaultCards, playerWho)){
                if(IsPlayerCardIncluded(straightCard3, defaultCards,playerWho)){
                        return "ThreeOfAKind";
                }else{  return "ThreeOfAKinX"; }
            } else
            if(ThreePairNoCheck(straightCard2, defaultCards, playerWho)){
                if(IsPlayerCardIncluded(straightCard2, defaultCards,playerWho)){
                        return "ThreeOfAKind";
                }else{  return "ThreeOfAKinX"; }
            } else
            if(ThreePairNoCheck(straightCard1, defaultCards, playerWho)){
                if(IsPlayerCardIncluded(straightCard1, defaultCards,playerWho)){
                        return "ThreeOfAKind";
                }else{  return "ThreeOfAKinX"; }
            }else    
            if(ThreePairNoCheck(straightCard, defaultCards, playerWho)){
                if(IsPlayerCardIncluded(straightCard, defaultCards,playerWho)){
                        return "ThreeOfAKind";
                }else{  return "ThreeOfAKinX"; }
            } else {            
                return "Nope";
            }
}

private bool ThreePairNoCheck(List<Card> pc, List<Card> defaultCards, string playerWho){

        //Debug.Log("ThreePair?");
            if(pc[0].value1 == pc[1].value1 && pc[1].value1 == pc[2].value1){                
               return true;
            } else{
                return false;
            }


} 

private bool ThreePair(List<Card> pc, List<Card> defaultCards, string playerWho){

        //Debug.Log("ThreePair?");
            if(pc[0].value1 == pc[1].value1 && pc[1].value1 == pc[2].value1){
                
                if(IsPlayerCardIncluded(pc,defaultCards,playerWho)){                    
                   // Debug.Log("ThreePair!!!");
                   MatchCardValueAndWho(pc[0].value1, playerWho);
                    return true ;
                }else{ 
                    return false; 
                    }

            } else{
                return false;
            }


}   


    private string TwoPairs(List<Card> pc, List<Card> defaultCards, string playerWho){

        
        int countCards = 0;
        int twoPairFound = 0;
        bool isCardThere = false;
        bool card1 =false;
        bool card2 =false;
        foreach(Card p in pc){
            countCards++;
            //Debug.Log("StraightLastCards:"+countCards + " : " + p.value1 + p.name );
        }
        pc.Reverse();
        
        countCards--;

            for (int i = 0; i < countCards ; i++){
                   if(pc[i].value1 == pc[i + 1].value1){
                      // Debug.Log("Match Found:" + pc[i].value1 + " & " + pc[i + 1].value1);
                            twoPairFound++;
                            if(defaultCards[0].value1 == pc[i].value1){ 
                                isCardThere = true;
                                card1 = true; 
                            }
                            if(defaultCards[1].value1 == pc[i].value1){ 
                                isCardThere = true;
                                card2 = true;
                                }                  
                   } else{
                       
                   }
            }    
            
        //Debug.Log("twoPairFound: " + twoPairFound);


        if(card1 && card2){
                if(defaultCards[0].value1 > defaultCards[1].value1){
                    MatchCardValueAndWho(defaultCards[0].value1, playerWho);
                    MatchCardValueAndWhoLower(defaultCards[1].value1, playerWho);
                }else{
                    MatchCardValueAndWho(defaultCards[1].value1, playerWho);
                    MatchCardValueAndWhoLower(defaultCards[0].value1, playerWho);
                }             
        }else if(card1){
                MatchCardValueAndWho(defaultCards[0].value1, playerWho);
                MatchCardValueAndWhoLower(0, playerWho);
        }else if(card2){
                MatchCardValueAndWho(defaultCards[1].value1, playerWho);
                MatchCardValueAndWhoLower(0, playerWho);
        }else{

        }
         

        if(twoPairFound > 1 && isCardThere ){
                return "TwoPairs";
        } else {
                return "Nope";
        }



    }

    private string OnePair(List<Card> pc, List<Card> defaultCards, string playerWho){

        bool card1 = false;
        bool card2 = false;

        foreach(Card p in publiccard){
               if(p.value1 == defaultCards[0].value1){ card1 = true; }
               if(p.value1 == defaultCards[1].value1){ card2 = true; } 
        }

        if(card1 && card2){
                if(defaultCards[0].value1 > defaultCards[1].value1){
                    MatchCardValueAndWho(defaultCards[0].value1, playerWho);
                }else{
                    MatchCardValueAndWho(defaultCards[1].value1, playerWho);
                }
            return "OnePair";    
        }
        else
        if(card1 || card2){
            return "OnePair";
        }else if(defaultCards[0].value1 == defaultCards[1].value1) 
            return "OnePair";
        else{
            return "Nope";
        }

    }

    private void MatchCardValueAndWho(int num, string playerWho){

            if(playerWho == "player1"){
                MatchCardValueAndWho1 = num;
                MatchValueWho1.text = num.ToString();
            }else{
                MatchCardValueAndWho2 = num;
                MatchValueWho2.text = num.ToString();
            }

    }

    private void MatchCardValueAndWhoLower(int num, string playerWho){

            if(playerWho == "player1"){
                MatchCardValueAndWho11 = num;
                MatchValueWho1.text = num.ToString();
            }else{
                MatchCardValueAndWho22 = num;
                MatchValueWho2.text = num.ToString();
            }

    }



    private int HighCard(List<Card> pc, string playerWho){

        Debug.Log(pc[0].value1 + " : " + pc[1].value1);

        
        if(playerWho == "player1"){
            return MatchCardValueAndWho1;
        } else {
            return MatchCardValueAndWho2; 
        }
        
    }

    private int HighCard2(List<Card> pc, string playerWho){

        Debug.Log(pc[0].value1 + " : " + pc[1].value1);        

         if(pc[0].value1 > pc[1].value1){
           //  MatchCardValueAndWho(pc[0].value1,playerWho);
             return pc[0].value1;
         }else{
           //  MatchCardValueAndWho(pc[1].value1,playerWho);
             return pc[1].value1;
         }

    }

    private int LowCard(List<Card> pc, string playerWho){

         if(pc[0].value1 > pc[1].value1){
           //  MatchCardValueAndWho(pc[1].value1,playerWho);
             return pc[1].value1;
         }else{
           //  MatchCardValueAndWho(pc[0].value1,playerWho);
             return pc[0].value1;
         }

    }


   



   
    IEnumerator ClearAll(){
            player1card.Clear();
            player2card.Clear();
            publiccard.Clear();
            playerClubs.Clear();
            playerDiamonds.Clear();
            playerHearts.Clear();
            playerSpades.Clear();
            player1publiccard.Clear();
            player2publiccard.Clear();
            gameResult.text = "";
            MatchValueWho1.text = "";
            MatchValueWho2.text = "";            
            TotalPlayer2Score = 0;
            TotalPlayer1Score = 0;
            MatchCardValueAndWho1 = 0;
            MatchCardValueAndWho2 = 0;
            MatchCardValueAndWho11 = 0;
            MatchCardValueAndWho22 = 0;
            takenValue1 = 0;
            takenValue2 = 0;
            player1Hand.text = "";
            player2Hand.text = "";
            gameResultPanel.SetActive(false);
            HighCardBreaker1 =false;
            HighCardBreaker2 =false;
            LowerBreakerCard1= 0;
            LowerBreakerCard2= 0;

            for(int i = 0; i < 9 ; i++ ){
                    ClearImages(allcardImages[i]);
            }
            yield return new WaitForSeconds(2.0f);
    }


     void ClearImages(Image cardImage){
         cardImage.GetComponent<Image>().sprite = BackCard;
     }



    public void PlayAgain(){

            PlayButton.SetActive(false);

           StartCoroutine( ClearAll());
            
            if(cardCount < 20){
            
            ButtonText.text = "Play Again";

            }


        if(cardCount < 9){            
            
            
            SceneManager.LoadScene("Poker");
            //card.Clear();
            //ReStart();

        } else
        {
             StartCoroutine(SetCard(card));
        }

    }


}
