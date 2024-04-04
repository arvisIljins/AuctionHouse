import { find } from "lodash";
import { create } from "zustand";

const initialState = {
  bids: [],
};

export const useBidsStore = create((set) => ({
  ...initialState,
  setBids: (bids) => {
    set(() => ({
      bids,
    }));
  },
  addBid: (bid) => {
    set((state) => ({
      bids: !state.bids.find((x) => x.id === bid.id)
        ? [bid, ...state.bids]
        : [...state.bids],
    }));
  },
}));
