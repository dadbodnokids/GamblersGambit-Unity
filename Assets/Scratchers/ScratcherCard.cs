using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Scott.ScratchCard;
using UnityEngine.Events;

public class ScratcherCard : MonoBehaviour
{
    public enum Symbol {
        Cherry = 0,
        Orange,
        Banana,
        COUNT,
    }

    public List<Symbol> symbols;

    public List<Sprite> symbolSprites;
    public List<Image> symbolImages;
    public List<ScratchMask> masks;

    public List<bool> scratched = new List<bool>() { false, false, false };

    public UnityEvent OnAllSegmentsScratched;

    // need an event when the scratching is complete

    private void Start() {
        SetRandomSymbols();
        SetSymbolImages();
    }

    public void SetSymbols(Symbol s1, Symbol s2, Symbol s3) {
        symbols[0] = s1;
        symbols[1] = s2;
        symbols[2] = s3;
        SetSymbolImages();
    }

    void SetRandomSymbols() {
        for(int i = 0; i < symbols.Count; i++) {
            Symbol symbol = (Symbol)Random.Range(0, (int)Symbol.COUNT);
            symbols[i] = symbol;
        }
    }

    void SetSymbolImages() {
        for(int i = 0; i < symbols.Count; i++) {
            Sprite sprite = symbolSprites[(int)symbols[i]];
            symbolImages[i].sprite = sprite;
        }
    }

    public void OnMaskScratched(ScratchMask scratchMask) {
        int index = masks.IndexOf(scratchMask);
        if(!scratched[index]) {
            scratched[index] = true;

            bool allScratched = true;
            for(int i = 0; i < scratched.Count; i++) {
                if(!scratched[i]) {
                    allScratched = false;
                    break;
                }
            }

            if (allScratched)
                OnAllSegmentsScratched.Invoke();
        }
    }
}
